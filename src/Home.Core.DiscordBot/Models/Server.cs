namespace Home.Core.DiscordBot.Models
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Discord.WebSocket;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Services;
    using Serilog;

    /// <summary>
    /// Represents a server (guild, actually).
    /// </summary>
    [DebuggerDisplay("{Codeword}")]
    public class Server
    {
        /// <summary>
        /// The Channels on this server.
        /// </summary>
        public ConcurrentDictionary<string, DiscordChannel> Channels { get; private set; } = new ConcurrentDictionary<string, DiscordChannel>();

        public string Codeword { get; set; }

        /// <summary>
        /// The Server ID of this server.
        /// </summary>
        public ulong ServerId { get; set; }
        public static Dictionary<ulong, Server> Guilds { get; private set; } = new Dictionary<ulong, Server>();

        private SocketGuild guild;

        /// <summary>
        /// Constructs a Server mock.
        /// </summary>
        /// <param name="settings">The settings of this server.</param>
        public Server(ServerInfo server) //, IShyCloudClient client)
        {
            this.ServerId = server.ServerId;
            this.Codeword = server.Codeword;
            //this.Client = client;

            Initialize(server);
        }

        private void Initialize(ServerInfo server)
        {
            this.GetGuild();

            // If you change the order here, you'll need to update the default warnIfMissing values below.
            var autoChannels = guild.TextChannels;
            foreach (var c in autoChannels)
            {
                GetChannel(c.Name, true);
            }

            foreach (var c in server.Channels)
            {
                GetChannel(c.Name, true);
            }
        }

        public async Task ArchiveAsync()
        {
            var channels = this.guild.TextChannels;
            foreach (var current in channels)
            {
                var messages = current.GetMessagesAsync(1000);
                await foreach (var mset in messages)
                {
                    foreach (var m in mset)
                    {
                        Debug.WriteLine(m.Content);
                    }
                }
            }
        }

        public SocketGuild GetGuild()
        {
            if (this.guild == null)
            {
                this.guild = DiscordService.Client.GetGuild(this.ServerId);
            }

            return this.guild;
        }

        /// <summary>
        /// Gets a channel on this server.
        /// TODO: Update this doc to talk about how this is intended for channel population from a list returned from DisordWebSocket API.
        /// </summary>
        /// <param name="channelName">The channel to get</param>
        /// <param name="ensure">Whether to ensure the channel asked about exists.</param>
        /// <param name="warnIfMissing">Warns if a channel is encountered that is not already known.</param>
        /// <remarks>warnIfMissing should be false on initialization and true for the rest of the program execution.</remarks>
        /// <returns>the channel if found or created. null otherwise.</returns>
        public DiscordChannel GetChannel(string channelName, bool ensure, bool warnIfMissing = false)
        {
            if (!this.Channels.ContainsKey(channelName))
            {
                if (ensure)
                {
                    if (Channels.ContainsKey(channelName))
                    {
                        return Channels[channelName];
                    }

                    if (warnIfMissing)
                    {
                        Log.Warning($"Channel {channelName} was not found in existing channels.");
                    }

                    var guild = this.GetGuild();
                    var channels = guild.TextChannels;
                    var channel = channels.First(c => c.Name == channelName);
                    var ch = new DiscordChannel(this.Codeword, channelName, channel);
                    Channels[channelName] = ch;
                }
                else
                {
                    return null;
                }
            }

            return Channels[channelName];
        }

        /// <summary>
        /// Gets a channel on this server.
        /// TODO: Update this doc to talk about how this is intended for channel population from config file, so we expect the channel to exist, but we will bail you out.
        /// </summary>
        /// <param name="channelName">The channel to get</param>
        /// <param name="ensure">Whether to ensure the channel asked about exists.</param>
        /// <param name="warnIfMissing">Warns if a channel is encountered that is not already known.</param>
        /// <remarks>warnIfMissing should be be true in most known scenarios.</remarks>
        /// <returns>the channel if found or created. null otherwise.</returns>
        public DiscordChannel GetChannel(ChannelSettings settings, bool ensure, bool warnIfMissing = true)
        {
            var channelName = settings.Name;

            if (!this.Channels.ContainsKey(channelName))
            {
                if (ensure)
                {
                    if (Channels.ContainsKey(channelName))
                    {
                        return Channels[channelName];
                    }

                    if (warnIfMissing)
                    {
                        Log.Warning($"Channel {channelName} was not found in existing channels.");
                    }

                    var guild = this.GetGuild();
                    var channels = guild.TextChannels;
                    var channel = channels.First(c => c.Name == channelName);
                    var ch = new DiscordChannel(this.Codeword, settings, channel);
                    Channels[channelName] = ch;
                }
                else
                {
                    return null;
                }
            }

            return Channels[channelName];
        }
    }
}
