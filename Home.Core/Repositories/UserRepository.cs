namespace Home.Core.Repositories
{
    using System.Threading.Tasks;
    using Home.Core.Models;

    public interface IUserRepository
    {
        Task EnsureUser(UserDto user);
    }

    public class UserRepository
    {
        public async Task EnsureUser(UserDto user)
        {
            return;
        }
    }
}
