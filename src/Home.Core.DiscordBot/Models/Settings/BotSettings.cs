namespace Home.Core.DiscordBot.Models.Settings
{ 
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Settings")]
    public record BotSettings
    {
        public ServerInfo MainServer { get; init; }

        public IList<ServerInfo> Servers { get; init; } //= new List<ServerInfo>();

        public string ArchiveFolder { get; init; }

        [DebuggerDisplay("SECRET")]
        public ulong BotId { get; init; }

        public string DemocracyChannelName { get; init; }

        [DebuggerDisplay("SECRET")]
        public string DiscordToken { get; init; }

        public string GoldfishChannelName { get; init; }

        [DebuggerDisplay("SECRET")]
        public string HomeServer { get; init; }

        public string TestSpaceChannelName { get; init; }

        public ulong GetHomeServerID()
        {
            if (ulong.TryParse(this.HomeServer, out ulong result))
            {
                return result;
            }

            return 0;
        }
    }
}