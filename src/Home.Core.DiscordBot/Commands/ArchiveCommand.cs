namespace Home.Core.DiscordBot.Commands
{
    using System;
    using System.Threading.Tasks;
    using Serilog;
    using Home.Core.Commands;

    public record ArchiveCommand : HomeCommand
    {
        public string ServerCodeword { get; init; }
        public static (string, int) DefaultStage = ("Archive", 1);

        public ArchiveCommand(string sc) : base(DefaultStage.Item1, DefaultStage.Item2)
        {
            this.ServerCodeword = sc;
            this.Stage = 1;
        }

        protected async override Task<StageExecutionResult> ExecuteStageAsync()
        {
            bool result = false;
            try
            {
                if (Stage == 1)
                {
                   //result = await ExecuteStageAsync();
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Issue in {command} for {codeword}", this.Command, this.ServerCodeword);
            }

            //return result;
            return DefaultResult;
        }
    }
}
