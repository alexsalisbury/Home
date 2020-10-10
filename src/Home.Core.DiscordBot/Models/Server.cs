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

    /// <summary>
    /// Represents a server (guild, actually).
    /// </summary>
    [DebuggerDisplay("{Codeword}")]
    public class Server
    {
        /// <summary>
        /// The Channels on this server.
        /// </summary>
       // public ConcurrentDictionary<string, DiscordChannel> Channels { get; private set; } = new ConcurrentDictionary<string, DiscordChannel>();

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

            this.guild = DiscordService.Client.GetGuild(this.ServerId);
            var channels = guild.TextChannels;

            foreach (var c in server.Channels)
            {
                var channel = channels.First(ch => ch.Name == c.Name);
               // this.Channels[c.Name] = new DiscordChannel(server, c, channel);
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

        ///// <summary>
        ///// Gets a channel on this server.
        ///// </summary>
        ///// <param name="channelName">The channel to get</param>
        ///// <param name="ensure">Whether to ensure the channel asked about exists.</param>
        ///// <returns>the channel if found or created. null otherwise.</returns>
        //public IDiscordChannel GetChannel(string channelName, bool ensure)
        //{
        //    if (!this.Channels.ContainsKey(channelName))
        //    {
        //        if (ensure)
        //        {
        //            if (Channels.ContainsKey(channelName))
        //            {
        //                return Channels[channelName];
        //            }

        //            var guild = DiscordClient.GetGuild(this.ServerId);
        //            var channels = guild.TextChannels;
        //            var channel = channels.First(c => c.Name == channelName);
        //            var ch = new DiscordChannel(Client, this.serverSettings, null, channel);
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
