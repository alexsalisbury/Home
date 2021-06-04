namespace Home.Core.Commands
{
    using System;
    using System.Threading.Tasks;
    using Home.Core.Models.Schedules;
    using Serilog;
    using Serilog.Events;

    public record LogWriteCommand : HomeCommand
    {
        private LogEventLevel level;
        private string message;
        public static (string, int) DefaultStage = ("LogWrite", 0);
        private SingleSchedule schedule;

        public LogWriteCommand(SingleSchedule sched, LogEventLevel lel, string message) : base(DefaultStage.Item1, DefaultStage.Item2)
        {
            this.Stage = 0;
            this.level = lel;
            this.message = message;
            this.schedule = sched;
        }

        protected override async Task<StageExecutionResult> ExecuteStageAsync()
        {
            if (this.Stage == 0)
            {
                try
                {
                    StageExecutionResult result = await LogAsync();
                    return result;
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Issue in {command}", this.Command);
                }
            }

            return DefaultResult;
        }

        public bool ShouldRun(DateTimeOffset target)
        {
            var next = this.schedule.GetNext(target);
            // Console.Write(target); // playing with formatting?
            //   Console.Write(" => ");
            //   Console.WriteLine(next);
            return next < target;
        }

        private async Task<StageExecutionResult> LogAsync()
        {
            await Task.Delay(1);
            Log.Write(level, message);
            return DefaultResult.MarkStageComplete(DateTime.UtcNow, true, true);
        }
    }
}
