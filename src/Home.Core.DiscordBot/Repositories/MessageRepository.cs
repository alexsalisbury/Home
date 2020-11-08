namespace Home.Core.DiscordBot.Repositories
{
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;
    using Home.Core.Repositories;
    using System.Threading.Tasks;

    public class MessageRepository : BaseRepository, IMessageRepository
    {
        public MessageRepository(string connstr) : base(connstr)
        {

        }
        public Task<bool> EnsureAsync(IMessageInfo record)
        {
            return Task.FromResult(false);
        }
    }
}
