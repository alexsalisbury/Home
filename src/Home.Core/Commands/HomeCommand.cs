namespace Home.Core.Commands
{
    using System;
    using System.Threading.Tasks;

    public abstract record HomeCommand
    {
        public Guid Identifier { get; init; }
        public int Stage { get; init; }

        /// <summary>
        /// The command this represents. 
        /// </summary>
        public string Command { get; init; }

        public HomeCommand(string command)
        {
            this.Command = command;
        }

        public abstract Task<StageExecutionResult> ExecuteCommandStageAsync();
    }
}
