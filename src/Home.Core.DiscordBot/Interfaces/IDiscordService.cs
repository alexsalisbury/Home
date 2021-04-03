namespace Home.Core.DiscordBot.Interfaces.Services
{
    using System.Threading.Tasks;
    using Home.Core.DiscordBot.Services;

    public interface IDiscordService
    {
        static IDiscordService GetDiscordService() => DiscordService.Service;
        Task StartAsync();
    }
}
