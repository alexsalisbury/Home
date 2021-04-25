namespace Home.Core.DiscordBot.Models
{
    using System.Threading.Tasks;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.Interfaces.Models;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.DiscordBot.Models.Settings;

    public class ChatChannel : IHasDto<IChannelInfo>
    {
        /// <summary>
        /// The name of this channel
        /// </summary>
        public string Name { get; init; }
        public ulong Id { get; init; }
        public string Codeword { get; init; }
        public IChannelInfo DtoCache => cacheValid ? cache : ToDto();

        private ChannelInfoDto cache;
        private ChannelSettings settings;

        private bool cacheValid = false;

        public ChatChannel(ulong id, string codeword, string name)
        {
            this.Id = id;
            this.Name = name;
            this.Codeword = codeword;
        }

        public ChatChannel(ChannelInfoDto channel)
        {
            cache = channel;
            this.Id = channel.Id;
            this.Name = channel.Name;
            this.Codeword = channel.Codeword;
            cacheValid = true;
        }

        public async Task AddSettingsAsync(ChannelSettings settings)
        {
            await Task.Delay(1);

            //TODO: Different channel subtypes may have additional config/state to load from ShyCloud. Do that here.
            this.settings = settings;
        }

        public IChannelInfo ToDto()
        {
            //TODO: Threadsafe? Idempotency helps mitigate that.
            if (!cacheValid)
            {
                cache =
                    new ChannelInfoDto()
                    {
                        Id = this.Id,
                        Name = this.Name,
                        Codeword = this.Codeword,
                    };

                cacheValid = true;
            }

            return cache;
        }
    }
}
