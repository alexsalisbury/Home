namespace Home.Core.DiscordBot.Repositories
{
    using System.Threading.Tasks;
    using Home.Core.Repositories;
    using Home.Core.DiscordBot.Interfaces.Models;
    using Home.Core.DiscordBot.Interfaces.Repositories;

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
