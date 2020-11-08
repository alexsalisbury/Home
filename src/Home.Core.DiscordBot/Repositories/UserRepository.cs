namespace Home.Core.DiscordBot.Repositories
{
    using System.Collections.Generic;
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

        public Task<bool> EnsureAsync(IUserInfo record)
        {
            return Task.FromResult(false);
        }

        public Task<IQueryable<IUserInfo>> Fetch()
        {
            var result = new List<UserInfoDto>();
            return Task.FromResult(result.AsQueryable<IUserInfo>());
        }

        public Task<IUserInfo> Fetch(int id)
        {
            return null;
        }

        public Task<IUserInfo> Fetch(ulong id)
        {
            return null;
        }
    }
}
