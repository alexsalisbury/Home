namespace Home.Core.DiscordBot.Interfaces.Repositories
{
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.Interfaces.Data.Actions;

    public interface IChannelRepository: IFetchable<IChannelInfo>, IEnsurable<IChannelInfo>, IPatchable<IChannelInfo>
    {

    }
}