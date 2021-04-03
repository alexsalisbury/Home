namespace Home.Core.Commands
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public abstract record HomeCommand
    {
        protected static StageExecutionResult DefaultResult = new StageExecutionResult()
        {
            IsComplete = false,
        };

        /// <summary>
        /// The command this represents. 
        /// </summary>
        public string Command { get; init; }
        public Guid Identifier { get; init; }
        public int Stage { get; init; }

        public HomeCommand(string command, int stage)
        {
            this.Command = command;
            this.Stage = stage;
        }

        public async Task<StageExecutionResult> ExecuteCommandStageAsync()
        {
            DateTimeOffset start = DateTimeOffset.UtcNow;
            DateTimeOffset? end = null;

            StageExecutionResult result = await ExecuteStageAsync();

            Log.Information("{command} executed stage {stage} with result {result}", Command, Stage, result);

            if (result.IsComplete)
            {
                Log.Information("{command} complete.", Command);
            }

            return result;
        }

        protected abstract Task<StageExecutionResult> ExecuteStageAsync();
    }
}
