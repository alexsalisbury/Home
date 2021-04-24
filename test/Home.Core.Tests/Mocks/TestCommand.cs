namespace Home.Core.Tests.Mocks
{
    using System;
    using Home.Core.Commands;
    using System.Threading.Tasks;

    public record TestCommand : HomeCommand
    {
        public bool FailNext { get; set; }
        public bool RetryAll { get; set; }
        public bool RetryNext { get; set; }
        public bool CompleteNext { get; set; }

        public TestCommand() : base("", 0)
        {
        }

        public TestCommand(StageExecutionResult previous) : base("", previous?.NewStage ?? 0, previous)
        {
        }

        protected override async Task<StageExecutionResult> ExecuteStageAsync()
        {
            var start = DateTimeOffset.UtcNow;
            bool success = true;

            if (RetryAll || RetryNext)
            {
                RetryNext = false;
                status = status.Retry(start);
            }
            else
            {
                if (FailNext)
                {
                    success = false;
                    FailNext = false;
                }

                status = status.MarkStageComplete(start, success, CompleteNext);
            }

            return status;
        }
    }
}