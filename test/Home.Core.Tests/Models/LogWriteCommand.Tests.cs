namespace Home.Core.Tests
{
    using Serilog.Events;
    using Xunit;
    using Home.Core.Commands;

    public class LogWriteCommand_Tests
    {
        [Fact]
        public void TestLogWriteCommand()
        {
            var tc = new LogWriteCommand(LogEventLevel.Information, "TestLogWriteCommand");
            Assert.NotNull(tc);
        }
    }
}
