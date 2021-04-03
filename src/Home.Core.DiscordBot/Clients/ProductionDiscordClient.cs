namespace Home.Core.DiscordBot.Clients
{
    using Discord;
    using Discord.WebSocket;
    using Home.Core.DiscordBot.Interfaces.Clients;

    public class ProductionDiscordClient : IMockableDiscordClient
    {
        public ProductionDiscordClient(DiscordSocketClient client)
        {
            IMockableDiscordClient.SetWrapper(client);
        }

        public void Dispose()
        {
            //TODO throw new System.NotImplementedException();
        }

        ConnectionState IMockableDiscordClient.GetConnectionState()
        {
            return IMockableDiscordClient.ClientWrapper.ConnectionState;
        }

        SocketGuild IMockableDiscordClient.LookupGuild(ulong serverId)
        {
            return IMockableDiscordClient.ClientWrapper.GetGuild(serverId);
        }
    }
}
