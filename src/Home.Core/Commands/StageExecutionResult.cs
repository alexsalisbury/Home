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

    /// <summary>
    /// This record is a result of a stage execution.
    /// </summary>
    public record StageExecutionResult
    {
        public bool Success { get; init; }
        public bool IsStageComplete { get; init; }
        public bool IsCommandComplete { get; init; }
        public int NewStage { get; init; }
        public int RetriesRemaining { get; init; }
        public DateTimeOffset CreatedAt { get; init; }
        public DateTimeOffset? StartedAt { get; init; }
        public DateTimeOffset? CompletedAt { get; init; }
        public DateTimeOffset? WaitUntil { get; init; }

        /// <summary>
        /// Sets StartedAt
        /// </summary>
        /// <returns>updated record.</returns>
        public StageExecutionResult Start()
        {
            return this with { StartedAt = DateTimeOffset.UtcNow};
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="start"></param>
        /// <returns></returns>
        public StageExecutionResult Retry(DateTimeOffset start)
        {
            if (RetriesRemaining > 0)
            {
                DateTimeOffset? waitUntil = DateTimeOffset.UtcNow.AddMinutes(1);
                return this with { StartedAt = StartedAt ?? start, CompletedAt = null, RetriesRemaining = this.RetriesRemaining - 1, WaitUntil = waitUntil };
            }

            return MarkStageComplete(start, false);
        }

        public StageExecutionResult MarkStageComplete(DateTimeOffset start, bool success, bool commandComplete = false)
        {
            return this with { IsStageComplete = true, IsCommandComplete = commandComplete, StartedAt = StartedAt ?? start, CompletedAt = DateTimeOffset.UtcNow, Success = success};
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
