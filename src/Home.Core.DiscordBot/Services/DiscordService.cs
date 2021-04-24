namespace Home.Core.DiscordBot.Services
{
    using System;
    using System.Threading.Tasks;
    using Discord;
    using Discord.WebSocket;
    using Serilog;
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Interfaces.Clients;

    public class DiscordService : IMockableDiscordService
    {
        public static IMockableDiscordService Service { get; private set; }
        public static bool IsConnected() => IMockableDiscordClient.IsConnected();
        //public static SocketGuild GetGuild(ulong serverId) => IMockableDiscordClient.GetGuild(serverId);

        //internal static IMockableDiscordClient Client { get; private set; }

        public static async Task StartAsync(BotSettings settings)
        {
            //    //Settings = settings;
            //    Service = new DiscordService();

            //    var existing = Client;
            //    var discordToken = settings.DiscordToken ?? Environment.GetEnvironmentVariable("DiscordToken");

            //    if (existing == null)
            //    {
            //        Client = Client ?? await CreateDiscordClient(discordToken);
            //    }    

            //    if (existing != Client)
            //    {
            //        existing?.Dispose();
            //    }

            //    await Task.Delay(500);
            //    await Service.StartAsync();
         }

        //public async Task HandleMessageAsync(SocketMessage arg)
        //{
        //}

        public Task StartAsync()
        {
            throw new NotImplementedException();
        }

        //internal static async Task<ProductionDiscordClient> CreateDiscordClient(string discordToken)
        //{
        //    return new ProductionDiscordClient(await CreateSocketClient(discordToken));
        //}

        //internal static async Task<DiscordSocketClient> CreateSocketClient(string discordToken)
        //{
        //    Log.Information($"Creating a Discord client.");
        //    var client = new DiscordSocketClient();
        //    client.Log += WriteLog;
        //    client.MessageReceived += Client_MessageReceivedAsync;
        //    //client.Disconnected += Client_DisconnectAsync;

        //    // Remember to keep token private or to read it from an external source! 
        //    // In this case, we are reading the token from usersecrets or an environment variable. 
        //    await client.LoginAsync(TokenType.Bot, discordToken);
        //    await client.StartAsync();
        //    return client;
        //}

        //private static async Task Client_MessageReceivedAsync(SocketMessage arg)
        //{
        //    //TODO: await Service.HandleMessageAsync(arg);
        //}

        //private static Task WriteLog(LogMessage message)
        //{
        //    message.WriteToSerilog();
        //    return Task.CompletedTask;
        //}
    }
}
