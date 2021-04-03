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
    using Serilog;
    using Home.Core.Commands;
    using Home.Core.DiscordBot.Commands;
    using Home.Core.DiscordBot.Interfaces.Clients;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Services;

    /// <summary>
    /// Represents a server (guild, actually).
    /// </summary>
    [DebuggerDisplay("{Codeword}")]
    public abstract class Server
    {
        public IEnumerable<string> Capabilities => channelSettings.Select(c => c.Name);
        public static Dictionary<string, Server> Guilds { get; private set; } = new Dictionary<string, Server>();
        /// <summary>
        /// The Channels on this server.
        /// </summary>
        public ConcurrentDictionary<string, DiscordChannel> Channels { get; private set; } = new ConcurrentDictionary<string, DiscordChannel>();
        public static Server GetServerByCodeword(string codeword) => Guilds.Values.FirstOrDefault(g => g.Codeword == codeword);

        public string Codeword { get; init; }

        protected IEnumerable<ChannelSettings> channelSettings;
        protected SocketGuild guild;

        /// <summary>
        /// The Server ID of this server.
        /// </summary>
        public ulong ServerId { get; init; }

        /// <summary>
        /// Constructs a Server mock.
        /// </summary>
        /// <param name="settings">The settings of this server.</param>
        public Server(ServerInfo server)
        {
            if (Guilds.ContainsKey(server.Codeword))
            {
                Log.Warning("Duplicate codeword {serverCodeword} found.", server.Codeword);
            }

            this.ServerId = server.ServerId;
            this.Codeword = server.Codeword;
            channelSettings = server.Channels;
            Guilds[server.Codeword] = this;
        }

        public async Task InitializeAsync()
        {
            this.GetGuild();
            var actualChannels = guild.TextChannels;

            foreach (var c in actualChannels)
            {
                var dc = GetChannel(c, true); //Normally false, but I want it noisy.
                var setting = channelSettings.FirstOrDefault(cs => cs.Name == c.Name);

                if (setting != null)
                {
                    await dc.AddSettingsAsync(setting);
                }
            }
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
        public DiscordChannel GetChannel(SocketTextChannel channel, bool ensure, bool warnIfMissing = false)
        {
            if (!this.Channels.ContainsKey(channel.Name))
            {
                if (ensure)
                {
                    if (Channels.ContainsKey(channel.Name))
                    {
                        return Channels[channel.Name];
                    }

                    if (warnIfMissing)
                    {
                        Log.Warning($"Channel {channel.Name} was not found in existing channels.");
                    }

                    var guild = this.GetGuild();
                    var ch = new DiscordChannel(this.Codeword, channel);
                    Channels[channel.Name] = ch;
                }
                else
                {
                    return null;
                }
            }

            return Channels[channel.Name];
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
                    var ch = new DiscordChannel(this.Codeword, channel);
                    Channels[channelName] = ch;
                }
                else
                {
                    return null;
                }
            }

            return Channels[channelName];
        }


        public SocketGuild GetGuild()
        {
            if (this.guild == null)
            {
                this.guild = DiscordService.GetGuild(this.ServerId);
            }

            return this.guild;
        }
    }
}