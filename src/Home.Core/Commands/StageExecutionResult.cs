namespace Home.Core.Commands
{
    using System;

    //public record CommandExecution
    //{
    //    public bool Success { get; init; }
    //    public bool IsComplete { get; init; }
    //    public int Stage { get; init; }
    //    public bool RetriesRemaining { get; init; }
    //    public DateTimeOffset CreatedAt { get; init; }
    //    public DateTimeOffset StartedAt { get; init; }
    //    public DateTimeOffset CompletedAt { get; init; }
    //    public DateTimeOffset WaitUntil { get; init; }
    //}

    // Initial effort. 
    // TODO: Implement CommandExecution, CommandExecutionResult, IShyEntity, db tables.

    public record StageExecutionResult
    {
        public bool Success { get; init; }
        public bool IsComplete { get; init; }
        public int NewStage { get; init; }
        public int RetriesRemaining { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? StartedAt { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }
        public DateTimeOffset? WaitUntil { get; init; }

        public StageExecutionResult Start()
        {
            return this with { StartedAt = DateTimeOffset.UtcNow};
        }

        public StageExecutionResult Retry()
        {
            return this with { StartedAt = null, CompletedAt = null, RetriesRemaining = this.RetriesRemaining - 1 };
        }

        public StageExecutionResult MarkComplete(bool success)
        {
            DateTimeOffset? waitUntil = success ? null : DateTimeOffset.UtcNow.AddMinutes(1);
            return this with { IsComplete = true, CompletedAt = DateTimeOffset.UtcNow, Success = success, WaitUntil = waitUntil };
        }
    }

    //// TODO: Planning
    //public record Stage
    //{ 
    //    public int Number { get; init; }
    //    public DateTimeOffset CreatedAt { get; init; }
    //    public DateTimeOffset? WaitUntil { get; init; }
    //}
}
