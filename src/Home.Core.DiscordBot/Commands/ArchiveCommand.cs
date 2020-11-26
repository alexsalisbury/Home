namespace Home.Core.DiscordBot.Commands
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.Commands;
    using Home.Core.DiscordBot.Models;
    using Serilog;

    internal record ArchiveCommand : HomeCommand
    {
        public string ServerCodeword { get; init; }
        
        internal ArchiveCommand(string sc) : base("Archive")
        {
            this.ServerCodeword = sc;
            this.Stage = 1;
        }

        public async override Task<StageExecutionResult> ExecuteCommandStageAsync()
        {
            DateTimeOffset start = DateTimeOffset.UtcNow;
            DateTimeOffset? end = null;
            bool result = false;
            bool isComplete = false;
            int newStage = Stage;

            try
            {
                if (Stage == 1)
                {
                    result = await CaptureMessagesAsync();

                    if (result)
                    {
                        end = DateTimeOffset.UtcNow;
                        isComplete = true; //IsComplete(Stage, result);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Issue in {command} for {codeword}", this.Command, this.ServerCodeword);
            }

            Log.Information("Archive executed stage {stage} with result {result}", Stage, result);

            if (isComplete)
            {
                Log.Information("Archive complete.");
            }

            return new StageExecutionResult()
            {
                CompletedAt = end,
                IsComplete = isComplete,
                NewStage = newStage,
                Success = result
            };
        }

        private async Task<bool> CaptureMessagesAsync()
        {
            var server = Server.Guilds?.Values?.First(g => g.Codeword == ServerCodeword);
            if (server != null)
            {
                var messages = server.CaptureMessagesAsync();
                await foreach (var m in messages)
                {

                }
            }

            return true;
        }
    }
}
