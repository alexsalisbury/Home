namespace Home.Core.DiscordBot.Models
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Discord;
    using Discord.WebSocket;
    using Home.Core.Commands;
    using Home.Core.DiscordBot.Commands;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Services;
    using Serilog;

    /// <summary>
    /// Represents a server (guild, actually).
    /// </summary>
    [DebuggerDisplay("{Codeword}")]
    public class Server
    {
        public ConcurrentDictionary<ulong, string> MessageHashCache = new ConcurrentDictionary<ulong, string>();
        /// <summary>
        /// The Channels on this server.
        /// </summary>
        public ConcurrentDictionary<string, DiscordChannel> Channels { get; private set; } = new ConcurrentDictionary<string, DiscordChannel>();

        public string Codeword { get; set; }

        /// <summary>
        /// The Server ID of this server.
        /// </summary>
        public ulong ServerId { get; set; }
        public static Dictionary<string, Server> Guilds { get; private set; } = new Dictionary<string, Server>();

        private SocketGuild guild;
        private IEnumerable<ChannelSettings> channelSettings;

        /// <summary>
        /// Constructs a Server mock.
        /// </summary>
        /// <param name="settings">The settings of this server.</param>
        public Server(ServerInfo server) //, IShyCloudClient client)
        {
            if (Guilds.ContainsKey(server.Codeword))
            {
                Log.Warning("Duplicate codeword {serverCodeword} found.", server.Codeword);
            }

            this.ServerId = server.ServerId;
            this.Codeword = server.Codeword;
            //this.Client = client;
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

        public async IAsyncEnumerable<IMessage> CaptureMessagesAsync()
        {
            var channels = this.guild.TextChannels;
            foreach (var current in channels)
            {
                Log.Information("Crawling messages for {channelCategoryName} {channelName}", current?.Category?.Name ?? "", current?.Name ?? "");
                var itemshandled = 0;
                var setshandled = 0;

                var messageCollection = current.GetMessagesAsync(1000);
                await foreach (var messageSet in messageCollection)
                {
                    setshandled += 1;
                    foreach (var msg in messageSet)
                    {
                        itemshandled += 1;
                        yield return msg;
                    }
                }

                if (itemshandled > 0)
                {
                    Log.Information("Channel {channelCategoryName}:{channelName} has {itemshandled} messages in {setshandled} sets.", current?.Category?.Name ?? "", current?.Name ?? "", itemshandled, setshandled);
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

        internal async Task<bool> StartArchiveAsync()
        {
            var archiveCommand = new ArchiveCommand(this.Codeword);
            //Enqueue(archiveCommand);
            //var stage = archiveCommand.GetNextStage(); //Still in planning. Not sure about this.
            var result =  await archiveCommand.ExecuteCommandStageAsync();
            return true;
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

        ///// <summary>
        ///// Gets a channel on this server.
        ///// TODO: Update this doc to talk about how this is intended for channel population from config file, so we expect the channel to exist, but we will bail you out.
        ///// </summary>
        ///// <param name="channelName">The channel to get</param>
        ///// <param name="ensure">Whether to ensure the channel asked about exists.</param>
        ///// <param name="warnIfMissing">Warns if a channel is encountered that is not already known.</param>
        ///// <remarks>warnIfMissing should be be true in most known scenarios.</remarks>
        ///// <returns>the channel if found or created. null otherwise.</returns>
        //public DiscordChannel GetChannel(ChannelSettings settings, bool ensure, bool warnIfMissing = true)
        //{
        //    var channelName = settings.Name;

        //    if (!this.Channels.ContainsKey(channelName))
        //    {
        //        if (ensure)
        //        {
        //            if (Channels.ContainsKey(channelName))
        //            {
        //                return Channels[channelName];
        //            }

        //            if (warnIfMissing)
        //            {
        //                Log.Warning($"Channel {channelName} was not found in existing channels.");
        //            }

        //            var guild = this.GetGuild();
        //            var channels = guild.TextChannels;
        //            var channel = channels.First(c => c.Name == channelName);
        //            var ch = new DiscordChannel(this.Codeword, settings, channel);
        //            Channels[channelName] = ch;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }

        //    return Channels[channelName];
        //}
    }
}
