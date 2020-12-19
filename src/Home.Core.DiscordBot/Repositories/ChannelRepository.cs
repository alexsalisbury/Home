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
            using (DbConnection dbConnection = (DbConnection)Connection)
            {
                string insertProcName = "sb_EnsureDiscordChannel";
                var k = new { info.GuildId, info.Id, info.Name };

                dbConnection.Open();
                await dbConnection.ExecuteAsync(insertProcName, k, commandType: CommandType.StoredProcedure);
                Log.Information($"Ensure completed: {insertProcName}:{k}");
                return true;
            }
        }

        public async Task<IQueryable<IChannelInfo>> Fetch()
        {
            using (DbConnection dbConnection = Connection)
            {
                string fetchProcName = "sb_FetchDiscordChannels";
                var result = await dbConnection.QueryAsync<ChannelInfoDto>(fetchProcName, commandType: CommandType.StoredProcedure);
                return result.AsQueryable();
            }
        }

        public Task<IChannelInfo> Fetch(ulong id)
        {
            return null;
        }

        //public Task<IChannelInfo> Patch(ulong id, IChannelInfo patch)
        //{
        //    return null;
        //}
    }
}
