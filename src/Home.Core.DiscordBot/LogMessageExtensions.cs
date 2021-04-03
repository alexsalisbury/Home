namespace Home.Core.DiscordBot
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Discord;
    using Serilog;
    using Serilog.Events;

    public static class LogMessageExtensions
    {
        public static void WriteToSerilog(this LogMessage message)
        {
            var sev = message.Severity;
            LogEventLevel level;

            switch (sev)
            {
                case LogSeverity.Critical:
                    level = LogEventLevel.Fatal;
                    break;
                case LogSeverity.Error:
                    level = LogEventLevel.Error;
                    break;
                case LogSeverity.Warning:
                    level = LogEventLevel.Warning;
                    break;
                case LogSeverity.Verbose:
                    level = LogEventLevel.Verbose;
                    break;
                case LogSeverity.Debug:
                    level = LogEventLevel.Debug;
                    break;
                default:
                    level = LogEventLevel.Information;
                    break;
            }

            if (message.Exception != null)
            {
                Log.Write(level, message.Message);
            }
            else
            {
                Log.Write(level, message.Exception, message.Message);
            }
        }
    }
}
