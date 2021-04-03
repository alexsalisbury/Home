namespace Home.Core.DiscordBot.Interfaces.Services
{
    using System.Threading.Tasks;

    public interface IArchiverServer
    {
        Task<bool> StartArchiveAsync();
    }
}
