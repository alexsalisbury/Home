namespace Home.Core.DiscordBot.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.Repositories;

    public class ChannelRepository : BaseRepository, IChannelRepository
    {
        public ChannelRepository(string connstr) : base(connstr)
        {

        }

        public Task<bool> EnsureAsync(IChannelInfo record)
        {
            return Task.FromResult(false);
        }

        public Task<IQueryable<IChannelInfo>> Fetch()
        {
            var result = new List<ChannelInfoDto>();
            return Task.FromResult(result.AsQueryable<IChannelInfo>());
        }

        public Task<IChannelInfo> Fetch(ulong id)
        {
            return null;
        }

        public Task<IChannelInfo> Patch(ulong id, IChannelInfo patch)
        {
            return null;
        }
    }
}
