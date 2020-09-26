namespace Home.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;
    using Home.Core.Models.Settings;

    public class ServerInfo
    {
        public ServerInfo()
        {

        }

        [JsonConstructor]
        public ServerInfo(List<ChannelSettings> channels)
        {
            Channels = channels.ToList();
        }

        public IEnumerable<ChannelSettings> Channels { get; set; }// = new List<ChannelSettings>();
        public string Codeword { get; set; }
        public string DiscordToken { get; set; }
        public ulong ServerId { get; set; }
    }
}
