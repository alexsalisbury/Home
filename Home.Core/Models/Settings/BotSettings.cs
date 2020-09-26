namespace Home.Core.Models.Settings
{ 
    using System.Collections.Generic;
    using System.Diagnostics;

    [DebuggerDisplay("Settings")]
    public class BotSettings
    {
        public ServerInfo MainServer { get; set; }

        public IList<ServerInfo> Servers { get; set; } //= new List<ServerInfo>();

        public string ArchiveFolder { get; set; }

        [DebuggerDisplay("SECRET")]
        public ulong BotId { get; set; }

        public string DemocracyChannelName { get; set; }

        [DebuggerDisplay("SECRET")]
        public string DiscordToken { get; set; }

        public string GoldfishChannelName { get; set; }

        [DebuggerDisplay("SECRET")]
        public string HomeServer { get; set; }

        public string TestSpaceChannelName { get; set; }

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