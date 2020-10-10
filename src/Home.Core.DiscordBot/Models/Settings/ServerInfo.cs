namespace Home.Core.DiscordBot.Models.Settings
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class ServerInfo
    {
        public IEnumerable<ChannelSettings> Channels { get; set; }// = new List<ChannelSettings>();
        public string Codeword { get; set; }
        public string DiscordToken { get; set; }
        public ulong ServerId { get; set; }

        public ServerInfo() { }

        [JsonConstructor]
        public ServerInfo(List<ChannelSettings> channels)
        {
            Channels = channels.ToList();
        }
    }
}
