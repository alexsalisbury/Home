namespace Home.Core.Tests.Mocks
{
    using System;
    using Home.Core.Commands;
    using System.Threading.Tasks;

    public record TestCommand : HomeCommand
    {
        public TestCommand() : base("")
        {

        }

        public override async Task<StageExecutionResult> ExecuteCommandStageAsync()
        {
            await Task.Delay(1);
            return new StageExecutionResult()
            {
                CompletedAt = DateTimeOffset.UtcNow,
                CreatedAt = DateTimeOffset.UtcNow,
                NewStage = 7,
                IsComplete = false,
                Success = true,
                RetriesRemaining = 0,
                StartedAt = DateTimeOffset.UtcNow
            };
        }
    }
}