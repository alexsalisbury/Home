namespace Home.Core.DiscordBot.Repositories
{
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.Repositories;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.DiscordBot.Models.Dtos;

    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(string connstr) : base(connstr)
        {

        }

        public UserRepository(IDbConnection conn) : base(conn)
        {

        }

        public Task<bool> EnsureAsync(IUserInfo record)
        {
            return Task.FromResult(false);
            //string procName = "tbd";
            //var p = new { record.Id, record.Name };

            //return await EnsureAsync(procName, p);
        }

        public Task<IQueryable<IUserInfo>> FetchAsync()
        {
            string procName = "";
            return FetchAsync<IUserInfo>(procName);
        }

        public Task<IUserInfo> FetchAsync(ulong id)
        {
            return null;
        }
    }
}
