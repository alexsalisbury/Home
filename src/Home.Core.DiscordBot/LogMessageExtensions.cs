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
            LogEventLevel level = message.Severity.GetSerilogEventLevel();

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
