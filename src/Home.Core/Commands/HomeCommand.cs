namespace Home.Core.Commands
{
    using System.Threading.Tasks;

    public abstract class HomeCommand
    {
        /// <summary>
        /// The command this represents. 
        /// </summary>
        public string Command { get; private set; }

        public HomeCommand(string command)
        {
            this.Command = command;
        }

        public abstract Task<bool> ExecuteCommandAsync();
    }
}
