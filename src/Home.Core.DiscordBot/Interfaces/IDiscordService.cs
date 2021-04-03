namespace Home.Core.DiscordBot.Interfaces.Services
{
    using Home.Core.DiscordBot.Services;

    public interface IDiscordService
    {
        static IDiscordService GetDiscordService() => DiscordService.Service;
    }
}
