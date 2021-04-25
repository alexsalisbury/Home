namespace Home.Core.DiscordBot.Models
{
    using Discord.WebSocket;
    using Home.Core.DiscordBot.Models.Dtos;

    /// <summary>
    /// Represents a Channel on a Discord Server.
    /// </summary>
    public class DiscordChannel : ChatChannel
    {
        public ISocketMessageChannel Channel { get; private set; }

        ////[JsonIgnore]
        ///// <summary>
        ///// The goldfish status, if any, of this channel.
        ///// </summary>
        ////public GoldfishStatus Fish { get; set; }

        /// <summary>
        /// Constructs a Channel from server
        /// </summary>
        /// <param name="channelName">The Channel Settings</param>
        public DiscordChannel(string codeword, ISocketMessageChannel channel) : base(channel?.Id ?? 0, codeword, channel?.Name ?? "Unknown" )
        {
            this.Channel = channel;
        }

        public DiscordChannel(ChannelInfoDto channel) : base(channel)
        {
        }
    }
}
