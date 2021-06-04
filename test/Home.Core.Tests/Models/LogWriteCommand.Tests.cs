namespace Home.Core.Tests
{
    using System;
    using System.Threading.Tasks;
    using Serilog.Events;
    using Xunit;
    using Home.Core.Commands;
    using Home.Core.Models.Schedules;

    public class LogWriteCommand_Tests
    {
        [Fact]
        public void TestLogWriteCommand()
        {
            var tc = new LogWriteCommand(new SingleSchedule(DateTimeOffset.UtcNow), LogEventLevel.Information, "TestLogWriteCommand");
            Assert.NotNull(tc);
        }
        [Fact]
        public async Task TestShouldRun()
        {
            var now = DateTimeOffset.UtcNow;
            var tc = new LogWriteCommand(new SingleSchedule(now.AddMinutes(1)), LogEventLevel.Information, "TestLogWriteCommand");
            Assert.NotNull(tc);
            Assert.False(tc.ShouldRun(now));
            Assert.True(tc.ShouldRun(now.AddMinutes(2)));

            //var r = await tc.ExecuteCommandStageAsync();
            //Assert.True(r.IsStageComplete);
            //Assert.False(tc.ShouldRun(now.AddMinutes(2)));
        }
    }
}
