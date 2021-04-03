namespace Home.Core.DiscordBot.Tests
{
    using Discord;
    using Serilog.Events;
    using Xunit;

    public class LogSeverityExtensions_Tests
    {
        [Fact]
        public void CheckLogSeverityCritical()
        {
            var ls = LogSeverity.Critical;
            var level = ls.GetSerilogEventLevel();
            Assert.Equal(LogEventLevel.Fatal, level);
        }

        [Fact]
        public void CheckLogSeverityDebug()
        {
            var ls = LogSeverity.Debug;
            var level = ls.GetSerilogEventLevel();
            Assert.Equal(LogEventLevel.Debug, level);
        }

        [Fact]
        public void CheckLogSeverityError()
        {
            var ls = LogSeverity.Error;
            var level = ls.GetSerilogEventLevel();
            Assert.Equal(LogEventLevel.Error, level);
        }

        [Fact]
        public void CheckLogSeverityInfo()
        {
            var ls = LogSeverity.Info;
            var level = ls.GetSerilogEventLevel();
            Assert.Equal(LogEventLevel.Information, level);
        }

        [Fact]
        public void CheckLogSeverityVerbose()
        {
            var ls = LogSeverity.Verbose;
            var level = ls.GetSerilogEventLevel();
            Assert.Equal(LogEventLevel.Verbose, level);
        }

        [Fact]
        public void CheckLogSeverityWarning()
        {
            var ls = LogSeverity.Warning;
            var level = ls.GetSerilogEventLevel();
            Assert.Equal(LogEventLevel.Warning, level);
        }
    }
}
