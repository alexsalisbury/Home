namespace Home.Core.DiscordBot.Interfaces.Repositories
{
    using Home.Core.Interfaces.Data.Actions;
    using Home.Core.DiscordBot.Interfaces.Models;

    public interface IUserRepository : IFetchable<IUserInfo>, IEnsurable<IUserInfo>
    {

    }
}