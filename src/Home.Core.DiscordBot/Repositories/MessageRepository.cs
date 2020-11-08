namespace Home.Core.DiscordBot.Repositories
{
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using System.Threading.Tasks;

    public class MessageRepository : IMessageRepository
    {
        public Task<bool> EnsureAsync(IMessageInfo record)
        {
            return Task.FromResult(false);
        }
    }
}
