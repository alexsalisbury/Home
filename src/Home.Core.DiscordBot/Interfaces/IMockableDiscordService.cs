namespace Home.Core.DiscordBot.Interfaces.Clients
{
    using System.Threading.Tasks;
    using Home.Core.DiscordBot.Interfaces.Services;

    public interface IMockableDiscordService : IDiscordService
    {
        internal Task StartAsync();
    }
}
