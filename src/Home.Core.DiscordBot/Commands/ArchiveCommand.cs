namespace Home.Core.DiscordBot.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.Commands;
    using Home.Core.DiscordBot.Models;
    using Serilog;

    internal class ArchiveCommand : HomeCommand
    {
        public string ServerCodeword { get; }
        
        internal ArchiveCommand(string sc) : base("Archive")
        {
            this.ServerCodeword = sc;
        }

        public async override Task<bool> ExecuteCommandAsync()
        {
            try
            {
                await Server.Guilds?.Values?.First(g => g.Codeword == ServerCodeword)?.ArchiveAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Issue in {command} for {codeword}", this.Command, this.ServerCodeword);
            }

            return false;
        }
    }
}
