namespace Home.Core.DiscordBot.Models
{
    using System.Threading.Tasks;
    using Discord.WebSocket;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.Interfaces.Models;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.DiscordBot.Models.Settings;

    /// <summary>
    /// Represents a Channel on a Discord Server.
    /// </summary>
    public class DiscordChannel : IHasDto<IChannelInfo>
    {
        /// <summary>
        /// The name of this channel
        /// </summary>
        public string Name { get; set; }
        public ISocketMessageChannel Channel { get; private set; }
        public string Codeword { get; private set; }
        private ChannelInfoDto cache;
        private ChannelSettings settings;
        public IChannelInfo DtoCache => cacheValid ? cache : ToDto();

        private bool cacheValid = false;
        //[JsonIgnore]
        /// <summary>
        /// The goldfish status, if any, of this channel.
        /// </summary>
        //public GoldfishStatus Fish { get; set; }

        /// <summary>
        /// Constructs a Channel from server
        /// </summary>
        /// <param name="channelName">The Channel Settings</param>
        public DiscordChannel(string codeword, ISocketMessageChannel channel)
        {
            this.Channel = channel;
            this.Codeword = codeword;
            this.Name = channel?.Name ?? "Unknown";
        }

        public async Task AddSettingsAsync(ChannelSettings settings)
        {
            await Task.Delay(1);

            //TODO: Different channel subtypes may have additional config/state to load from ShyCloud. Do that here.

            //this.settings = settings;
            //if (settings?.Fish != null)
            //{
            //    this.Fish = new GoldfishStatus(settings.Fish);
            //    this.Fish.IsAwake = true;
            //}
        }

        public IChannelInfo ToDto()
        {
            //TODO: Threadsafe? Idempotency helps mitigate that.
            if (!cacheValid)
            {
                cache =
                    new ChannelInfoDto()
                    {
                        Id = Channel.Id,
                        Name = Channel.Name,
                        Codeword = this.Codeword,
                    };

                cacheValid = true;
            }

            return cache;
        }
    }
}
