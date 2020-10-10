namespace Home.Service.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Discord;
    using Discord.WebSocket;
    using Serilog;
    using Serilog.Events;
    using Home.Core.Models.Settings;
    using Home.Core.Models;

    public class DiscordService
    {
        internal static DiscordSocketClient Client { get; private set; }
        public Dictionary<string, ServerInfo> Servers { get; }

        public DiscordService(BotSettings settings)
        {
            var servers = settings.Servers ?? new List<ServerInfo>() { settings.MainServer };
            Log.Information($"Loading {servers?.Count()} servers");

            foreach (var s in servers)
            {
                Servers.Add(s.Codeword, s);
            }
        }

        public static async Task StartAsync(BotSettings settings)
        {
            var existing = Client;
            var discordToken = settings.DiscordToken ?? Environment.GetEnvironmentVariable("DiscordToken");
            Client = Client ?? await CreateClient(discordToken);

            if (existing != Client)
            {
                existing?.Dispose();
            }
        }

        internal static async Task<DiscordSocketClient> CreateClient(string discordToken, bool force = false)
        {
            if (!force && Client != null)
            {
                return Client;
            }

            //Log.Information($"Creating a Client for {this.server.Codeword}");
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

        internal static Task ArchiveAsync()
        {
            return Task.CompletedTask;
        }

        private static void Clear()
        {
        }

        private static Task Disconnect(Exception arg)
        {
            Log.Error(arg, "Disconn");
            Clear();
            return Task.CompletedTask;
        }

        private static Task Client_MessageReceived(SocketMessage arg)
        {
            try
            {
                // If the message was not in the cache, downloading it will result in getting a copy of `after`.
                var message = arg;
                var channel = (SocketTextChannel)message.Channel;

                var usertype = message.Author.IsBot ? "Bot" : "User";
                Log.Information($"{usertype} {message.Author.Username} message {message.Content} created at {message.CreatedAt.UtcDateTime}");

                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Failed to handle message.");
                return Task.CompletedTask;
            }
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
