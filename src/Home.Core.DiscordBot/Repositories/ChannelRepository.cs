namespace Home.Core.DiscordBot.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.Repositories;
    using Serilog;

    public class ChannelRepository : BaseRepository, IChannelRepository
    {
        public ChannelRepository(string connstr) : base(connstr)
        {

        }

        public async Task<bool> EnsureAsync(IChannelInfo info)
        {
            string insertProcName = "sb_EnsureDiscordChannel";
            var p = new { info.GuildId, info.Id, info.Name };

            await EnsureAsync(insertProcName, p);
            return true;
        }

        public async Task<IQueryable<IChannelInfo>> FetchAsync()
        {
            string fetchProcName = "sb_FetchDiscordChannels";
            return await FetchAsync<IChannelInfo>(fetchProcName);
        }

        public Task<IChannelInfo> FetchAsync(ulong id)
        {
            return null;
        }

        //public Task<IChannelInfo> Patch(ulong id, IChannelInfo patch)
        //{
        //    return null;
        //}
    }
}
