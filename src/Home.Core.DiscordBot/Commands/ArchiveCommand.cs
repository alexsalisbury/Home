namespace Home.Core.DiscordBot.Commands
{
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.Commands;
    using Home.Core.DiscordBot.Models;

    internal class ArchiveCommand : HomeCommand
    {
        public string ServerCodeword { get; }
        
        internal ArchiveCommand(string sc) : base("Archive")
        {
            this.ServerCodeword = sc;
        }

        public async override Task<bool> ExecuteCommandAsync()
        {
            await Server.Guilds?.Values?.First(g => g.Codeword == ServerCodeword)?.ArchiveAsync();
            return true;
        }
    }
}
