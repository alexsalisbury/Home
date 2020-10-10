namespace Home.Core.DiscordBot.Commands
{
    using System.Threading.Tasks;
    using Discord.WebSocket;
    using Home.Core.Commands;
    using Home.Core.DiscordBot.Services;

    internal class ArchiveCommand : HomeCommand
    {
        public string ServerCodeword { get; }
        public ISocketMessageChannel MessageChannel { get; }
        
        internal ArchiveCommand(string sc, ISocketMessageChannel mc) : base("Archive")
        {
            this.ServerCodeword = sc;
            this.MessageChannel = mc;
        }

        public async override Task<bool> ExecuteCommandAsync()
        {
            return await DiscordService.ArchiveAsync();
        }
    }
}
