namespace Home.Core.DiscordBot.Models
{
    using Discord.WebSocket;
    using Home.Core.DiscordBot.Models.Settings;

    /// <summary>
    /// Represents a Channel on a Discord Server.
    /// </summary>
    public class DiscordChannel
    {
        /// <summary>
        /// The name of this channel
        /// </summary>
        public string Name { get; set; }
        public ISocketMessageChannel Channel { get; private set; }
        public string Codeword { get; private set; }

        //[JsonIgnore]
        /// <summary>
        /// The goldfish status, if any, of this channel.
        /// </summary>
        //public GoldfishStatus Fish { get; set; }

        /// <summary>
        /// Constructs a Channel from settings
        /// </summary>
        /// <param name="settings">The Channel Settings</param>
        public DiscordChannel(string codeword, ChannelSettings settings, ISocketMessageChannel channel)
        {
            this.Channel = channel;
            this.Codeword = codeword;
            this.Name = settings.Name;

            //if (settings?.Fish != null)
            //{
            //    this.Fish = new GoldfishStatus(settings.Fish);
            //    this.Fish.IsAwake = true;
            //}
        }

        /// <summary>
        /// Constructs a Channel from settings
        /// </summary>
        /// <param name="settings">The Channel Settings</param>
        public DiscordChannel(string codeword, string channelName, ISocketMessageChannel channel)
        {
            this.Channel = channel;
            this.Codeword = codeword;
            this.Name = channelName;
        }
    }
}
