namespace Home.Core.DiscordBot.Interfaces.Repositories
{
    using Home.Core.Interfaces.Data.Actions;
    using Home.Core.DiscordBot.Interfaces.Models;

    public interface IChannelRepository : IFetchable<IChannelInfo, ulong>, IEnsurable<IChannelInfo>
    {

    }
}