namespace Home.Core.Commands
{
    using System;
    using System.Threading.Tasks;
    using Serilog;

    public abstract record HomeCommand
    {
        protected static StageExecutionResult DefaultResult = new StageExecutionResult()
        {
            IsCommandComplete = false,
            IsStageComplete = false,
        };

        protected StageExecutionResult status;

        /// <summary>
        /// The command this represents. 
        /// </summary>
        public string Command { get; init; }
        public Guid Identifier { get; init; }
        public int Stage { get; init; }

        public HomeCommand(string command, int stage, StageExecutionResult previous = null)
        {
            this.Command = command;
            this.Stage = stage;
            Identifier = Guid.NewGuid();

            if (previous != null)
            {
                status = DefaultResult with
                {
                    CreatedAt = DateTime.UtcNow,
                    RetriesRemaining = previous.RetriesRemaining
                };
            }
            else
            {
                status = DefaultResult with
                {
                    CreatedAt = DateTime.UtcNow,
                    RetriesRemaining = 3
                };
            }
        }

        protected abstract Task<StageExecutionResult> ExecuteStageAsync();

        public async Task<StageExecutionResult> ExecuteCommandStageAsync()
        {
            StageExecutionResult result = await ExecuteStageAsync();
            Log.Information("{command} executed stage {stage} with result {result}", Command, Stage, result);

            if (result.IsCommandComplete)
            {
                Log.Information("{command} complete.", Command);
            }

            return result;
        }

    }
}
