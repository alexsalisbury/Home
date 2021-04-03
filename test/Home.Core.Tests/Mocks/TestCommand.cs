namespace Home.Core.Tests.Mocks
{
    using System;
    using Home.Core.Commands;
    using System.Threading.Tasks;

    public record TestCommand : HomeCommand
    {
        public bool FailNext { get; set; }
        public bool CompleteNext { get; set; }

        private int maxRetries = 3;
        private int retries;

        public TestCommand() : base("", 0)
        {
            Identifier = Guid.NewGuid();
            Stage = 0;
            retries = 3;
        }

        public TestCommand(StageExecutionResult previous) : base("", 0)
        {
            Identifier = Guid.NewGuid();
            Stage = previous.NewStage;
            retries = previous.RetriesRemaining;
        }

        protected override async Task<StageExecutionResult> ExecuteStageAsync()
        {
            var start = DateTimeOffset.UtcNow;
            bool success = true;

            if (FailNext)
            {
                success = false;
                FailNext = false;
            }
            else
            {
                await Task.Delay(1);
            }

            return DefaultResult with
            {
                CreatedAt = start,
                IsComplete = CompleteNext,
                RetriesRemaining = success ? maxRetries : retries-1,
                Success = success,
                CompletedAt = CompleteNext ? DateTimeOffset.UtcNow : null,
            };
        }
    }
}