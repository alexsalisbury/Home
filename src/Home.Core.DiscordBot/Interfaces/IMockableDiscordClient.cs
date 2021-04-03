namespace Home.Core.DiscordBot.Interfaces.Clients
{
    using Discord;
    using Discord.WebSocket;

    public interface IMockableDiscordClient
    {
        public static SocketGuild GetGuild(ulong serverId) => ClientOverride?.LookupGuild(serverId) ?? ClientWrapper?.GetGuild(serverId);
        public static bool IsConnected() => (ClientWrapper?.ConnectionState ?? ClientOverride.GetConnectionState()) == ConnectionState.Connected;

        protected static DiscordSocketClient ClientWrapper { get; set; }
        protected static IMockableDiscordClient ClientOverride { get; set; }

        protected abstract SocketGuild LookupGuild(ulong serverId);
        protected abstract ConnectionState GetConnectionState();

        public static void SetOverride(IMockableDiscordClient clientOverride)
        {
            ClientOverride = clientOverride;
        }

        public static void SetWrapper(DiscordSocketClient client)
        {
            ClientWrapper = client;
        }

        void Dispose();
    }
}
