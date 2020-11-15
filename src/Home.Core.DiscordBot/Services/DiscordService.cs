namespace Home.Core.DiscordBot.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Discord;
    using Discord.WebSocket;
    using Serilog;
    using Serilog.Events;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Models;

    public class DiscordService
    {
        private static BotSettings Settings;
        public static DiscordService Service { get; private set; }
        internal static DiscordSocketClient Client { get; private set; }
        internal Dictionary<string, Server> Servers { get; } = new Dictionary<string, Server>();
        internal Dictionary<ulong, string> ServerLookup { get; } = new Dictionary<ulong, string>();

        private DiscordService(BotSettings settings)
        {
            var servers = settings.Servers ?? new List<ServerInfo>() { settings.MainServer };
            Log.Information("Loading {serverCount} servers", servers?.Count());

            foreach (var s in servers)
            {
                Servers.Add(s.Codeword, new Server(s));
            }
        }

        public static async Task StartAsync(BotSettings settings)
        {
            Settings = settings;
            Service = new DiscordService(settings);
            var existing = Client;
            var discordToken = settings.DiscordToken ?? Environment.GetEnvironmentVariable("DiscordToken");
            Client = Client ?? await CreateClient(discordToken);

            if (existing != Client)
            {
                existing?.Dispose();
            }

            await Task.Delay(2000);
            await Service.StartAsync();
        }

        internal async Task StartAsync()
        {
            foreach (var s in Servers.Values)
            {
                s?.Initialize();
            }
        }

        internal static async Task<DiscordSocketClient> CreateClient(string discordToken, bool force = false)
        {
            if (!force && Client != null)
            {
                return Client;
            }

            Log.Information($"Creating a Discord client.");
            var client = new DiscordSocketClient();
            client.Log += WriteLog;
            client.MessageReceived += Client_MessageReceived;
            client.Disconnected += Disconnect;

            // Remember to keep token private or to read it from an external source! 
            // In this case, we are reading the token usersecrets or from an environment variable. 
            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();
            Client = client;
            return client;
        }

        public static bool IsConnected()
        {
            return Client.ConnectionState == ConnectionState.Connected;
        }

        public async Task<bool> ArchiveAsync()
        {
            foreach (var s in Servers.Values)
            {
                await s.ArchiveAsync();
            }

            return true;
        }
        private static Task Disconnect(Exception arg)
        {
            Log.Error(arg, "Disconn");
            return Task.CompletedTask;
        }

        private static async Task Client_MessageReceived(SocketMessage arg)
        {
            await Service.HandleMessage(arg);
        }

        private async Task HandleMessage(SocketMessage arg)
        {
            try
            {
                // If the message was not in the cache, downloading it will result in getting a copy of `after`.
                var msg = arg;
                var channel = (SocketTextChannel)msg.Channel;
                var usertype = msg.Author.IsBot ? "Bot" : "User";
                Log.Information("{usertype} {username} message {content} created at {createdAt}", usertype, msg.Author.Username, msg.Content, msg.CreatedAt.UtcDateTime);

                if (msg.Source == MessageSource.User)
                {
                    var server = GetServer(channel.Guild.Id);
                    if (server == null)
                    {
                        Log.Error("Message from unknown server.");
                        return;
                    }

                   //var user = server?.ServerUsers?.GetOrAdd(message.Author.Id, GenerateUser(message));
                    var discordChannel = server.GetChannel(channel.Name, true);

                    //TODO: await TryWake();
                    //user?.HandleMessage(message, discordChannel, Settings);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to handle message.");
            }
        }

        private Server GetServer(ulong serverId)
        {
            if (!ServerLookup.ContainsKey(serverId))
            {
                var codeword = Servers.Values.First(s => s.ServerId == serverId)?.Codeword;
                if (string.IsNullOrWhiteSpace(codeword))
                {
                    Log.Warning("Unknown serverid {serverId}", serverId);
                }
                else
                {
                    ServerLookup.Add(serverId, codeword);
                }
            }

            var cw = ServerLookup[serverId];
            return Servers[cw];
        }

        private static Task WriteLog(LogMessage message)
        {
            var sev = message.Severity;
            LogEventLevel level;

            switch (sev)
            {
                case LogSeverity.Critical:
                    level = LogEventLevel.Fatal;
                    break;
                case LogSeverity.Error:
                    level = LogEventLevel.Error;
                    break;
                case LogSeverity.Warning:
                    level = LogEventLevel.Warning;
                    break;
                case LogSeverity.Verbose:
                    level = LogEventLevel.Verbose;
                    break;
                case LogSeverity.Debug:
                    level = LogEventLevel.Debug;
                    break;
                default:
                    level = LogEventLevel.Information;
                    break;
            }

            if (message.Exception != null)
            {
                Log.Write(level, message.Message);
            }
            else
            {
                Log.Write(level, message.Exception, message.Message);
            }

            return Task.CompletedTask;
        }
    }
}
