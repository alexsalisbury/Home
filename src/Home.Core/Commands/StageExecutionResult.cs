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
    }

    //// TODO: Planning
    //public record Stage
    //{ 
    //    public int Number { get; init; }
    //    public DateTimeOffset CreatedAt { get; init; }
    //    public DateTimeOffset? WaitUntil { get; init; }
    //}
}
