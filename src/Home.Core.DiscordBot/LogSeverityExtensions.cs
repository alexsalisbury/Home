namespace Home.Core.DiscordBot
{
    using Discord;
    using Serilog.Events;

    public static class LogSeverityExtensions
    {
        public static LogEventLevel GetSerilogEventLevel(this LogSeverity sev)
        {
            return sev switch
            {
                LogSeverity.Critical => LogEventLevel.Fatal,
                LogSeverity.Error => LogEventLevel.Error,
                LogSeverity.Warning => LogEventLevel.Warning,
                LogSeverity.Verbose => LogEventLevel.Verbose,
                LogSeverity.Debug => LogEventLevel.Debug,
                _ => LogEventLevel.Information
            };
        }
    }
}
