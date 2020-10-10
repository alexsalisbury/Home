//namespace Home.Core.DiscordBot.Models
//{
//    using System;
//    using System.Collections.Concurrent;
//    using System.Collections.Generic;
//    using System.Linq;
//    using System.Threading;
//    using Discord.WebSocket;
//    using Shy.Core.Commands;
//    using Shy.Core.Interfaces;
//    using Shy.Core.Interfaces.Commands;
//    using Shy.Core.Interfaces.Configuration;
//    using Shy.Core.Interfaces.Infra;

//    /// <summary>
//    /// Represents a server (guild, actually).
//    /// </summary>
//    public class Server : IServer
//    {
//        /// <summary>
//        /// The Discord connection for this server.
//        /// </summary>
//        public static DiscordSocketClient DiscordClient { get; set; }

//        /// <summary>
//        /// The Channels on this server.
//        /// </summary>
//        public ConcurrentDictionary<string, IDiscordChannel> Channels { get; private set; } = new ConcurrentDictionary<string, IDiscordChannel>();

//        public string Codeword { get; set; }

//        /// <summary>
//        /// The Commands in this server's cache.
//        /// </summary>
//        public ConcurrentQueue<IExecutable> CommandQueue { get; private set; } = new ConcurrentQueue<IExecutable>();

//        /// <summary>
//        /// The Server ID of this server.
//        /// </summary>
//        public ulong ServerId { get; set; }

//        private IServerDetails serverSettings;

//        private IShyCloudClient Client { get; set; }

//        /// <summary>
//        /// The settings to connect to discord.
//        /// </summary>
//        private IBotSettings Settings;

//        /// <summary>
//        /// Constructs a Server mock.
//        /// </summary>
//        /// <param name="settings">The settings of this server.</param>
//        public Server(IBotSettings settings, IServerInfo server, IShyCloudClient client)
//        {
//            this.serverSettings = server;
//            this.Settings = settings;
//            this.ServerId = server.ServerId;
//            this.Codeword = server.Codeword;
//            this.Client = client;

//            var guild = DiscordClient.Guilds.First(g => g.Id == server.ServerId);
//            while (!guild.IsConnected)
//            {
//                Thread.Sleep(100);
//            }

//            var channels = guild.TextChannels;

//            foreach (var c in server.Channels)
//            {
//                var channel = channels.First(ch => ch.Name == c.Name);
//                this.Channels[c.Name] = new DiscordChannel(Client, server, c, channel);
//            }
//        }

//        /// <summary>
//        /// Gets a channel on this server.
//        /// </summary>
//        /// <param name="channelName">The channel to get</param>
//        /// <param name="ensure">Whether to ensure the channel asked about exists.</param>
//        /// <returns>the channel if found or created. null otherwise.</returns>
//        public IDiscordChannel GetChannel(string channelName, bool ensure)
//        {
//            if (!this.Channels.ContainsKey(channelName))
//            {
//                if (ensure)
//                {
//                    if (Channels.ContainsKey(channelName))
//                    {
//                        return Channels[channelName];
//                    }

//                    var guild = DiscordClient.GetGuild(this.ServerId);
//                    var channels = guild.TextChannels;
//                    var channel = channels.First(c => c.Name == channelName);
//                    var ch = new DiscordChannel(Client, this.serverSettings, null, channel);
//                    Channels[channelName] = ch;
//                }
//                else
//                {
//                    return null;
//                }
//            }

//            return Channels[channelName];
//        }

//        /// <summary>
//        /// Returns necessary commands for this server based on current state.
//        /// </summary>
//        /// <returns>A list of commands.</returns>
//        public IEnumerable<IExecutable> GetCommands()
//        {
//            var commands = new List<IExecutable>();

//            foreach (var k in Channels.Keys)
//            {
//                var current = Channels[k];
//                if (current?.Fish != null)
//                {
//                    var status = current.Fish;
//                    if (status?.IsAwake ?? false)
//                    {
//                        //TODO:
//                        var channelCommands = current.GetCommands();
//                        commands.AddRange(channelCommands);
//                        //goldfish.SaveStatus(status);
//                    }
//                }
//            }

//            return commands;
//        }

//        /// <summary>
//        /// Enqueues a Command in local cache.
//        /// </summary>
//        /// <param name="command">The command to cache.</param>
//        public void Enqueue(IExecutable command)
//        {
//            if (command != null)
//            {
//                this.CommandQueue.Enqueue(command);
//            }
//        }
//    }
//}
