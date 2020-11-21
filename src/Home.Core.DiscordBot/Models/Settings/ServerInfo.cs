namespace Home.Core.DiscordBot.Models.Settings
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Newtonsoft.Json;

    [DebuggerDisplay("{Codeword} (info)")]
    public record ServerInfo
    {
        public IEnumerable<ChannelSettings> Channels { get; init; }// = new List<ChannelSettings>();
        public string Codeword { get; init; }
        public string DiscordToken { get; init; }
        public ulong ServerId { get; init; }

        public ServerInfo() { }

        [JsonConstructor]
        public ServerInfo(List<ChannelSettings> channels)
        {
            Channels = channels.ToList();
        }
    }
}
