namespace Home.Core.Tests.Mocks
{
    using System;
    using Home.Core.Commands;
    using System.Threading.Tasks;

    public record TestCommand : HomeCommand
    {
        private static StageExecutionResult DefaultResult = new StageExecutionResult()
        {
            NewStage = 7,
            IsComplete = false,
        };

        private static StageExecutionResult CurrentResult => DefaultResult with {CreatedAt = DateTimeOffset.UtcNow, RetriesRemaining = 3 };
        private StageExecutionResult result;

        public bool FailNext { get; set; }

        public TestCommand() : base("")
        {
            result = CurrentResult;
        }

        public TestCommand(StageExecutionResult previous) : base("")
        {
            result = previous.Retry();
        }

        public override async Task<StageExecutionResult> ExecuteCommandStageAsync()
        {
            if (FailNext)
            {
                FailNext = false;
                result = result.MarkComplete(false);
            }
            else
            {
                bool success = true;
                result = result.Start();
                await Task.Delay(1);
                result = result.MarkComplete(success);
            }

            return result;
        }
    }
}