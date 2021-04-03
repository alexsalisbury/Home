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
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Models;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.DiscordBot.Interfaces.Services;
    using Home.Core.DiscordBot.Interfaces.Clients;

    public class DiscordService : IDiscordService
    {
        public static DiscordService Service { get; private set; }
        internal static DiscordSocketClient Client { get; private set; }

        public static bool IsConnected() => Client.ConnectionState == ConnectionState.Connected;
        public static SocketGuild GetGuild(ulong serverId) => Client.GetGuild(serverId);


        public static async Task StartAsync(BotSettings settings)
        {
            //Settings = settings;
            Service = new DiscordService();

            var existing = Client;
            var discordToken = settings.DiscordToken ?? Environment.GetEnvironmentVariable("DiscordToken");
            Client = Client ?? await CreateClient(discordToken);

            if (existing != Client)
            {
                existing?.Dispose();
            }

            await Task.Delay(500);
            await Service.StartAsync();
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
            client.MessageReceived += Client_MessageReceivedAsync;
            //client.Disconnected += Client_DisconnectAsync;

            // Remember to keep token private or to read it from an external source! 
            // In this case, we are reading the token from usersecrets or an environment variable. 
            await client.LoginAsync(TokenType.Bot, discordToken);
            await client.StartAsync();
            Client = client;
            return client;
        }

        private static async Task Client_MessageReceivedAsync(SocketMessage arg)
        {
            await Service.HandleMessageAsync(arg);
        }

        public async Task HandleMessageAsync(SocketMessage arg)
        {
        }

        private static Task WriteLog(LogMessage message)
        {
            message.WriteToSerilog();
            return Task.CompletedTask;
        }


        private async Task StartAsync()
        {
        }
    }
}
