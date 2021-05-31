namespace Home.Core.Commands
{
    using System;
    using System.Threading.Tasks;
    using Serilog;
    using Serilog.Events;

    public record LogWriteCommand : HomeCommand
    {
        private LogEventLevel level;
        private string message;
        public static (string, int) DefaultStage = ("LogWrite", 0);

        public LogWriteCommand(LogEventLevel lel, string message) : base(DefaultStage.Item1, DefaultStage.Item2)
        {
            this.Stage = 0;
            this.level = lel;
            this.message = message;
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


        private async Task<StageExecutionResult> LogAsync()
        {
            await Task.Delay(1);
            Log.Write(level, message);
            return DefaultResult.MarkStageComplete(DateTime.UtcNow, true, true);
        }
    }
}
