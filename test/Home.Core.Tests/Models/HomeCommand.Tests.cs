namespace Home.Core.Tests
{
    using Xunit;
    using Home.Core.Tests.Mocks;
    using System.Threading.Tasks;

    public class HomeCommand_Tests
    {
        [Fact]
        public void TestHomeCommand()
        {
            var tc = new TestCommand();
            Assert.NotNull(tc);
        }

        [Fact]
        public async Task ExecuteTestCommand()
        {
            var tc = new TestCommand();
            var result = await tc.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsComplete);
            Assert.Equal(3, result.RetriesRemaining);
        }

        [Fact]
        public async Task RetryTestCommand()
        {
            var tc = new TestCommand();
            tc.FailNext = true;
            var failResult = await tc.ExecuteCommandStageAsync();
            var tc2 = new TestCommand(failResult);
            var result = await tc2.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsComplete);
            Assert.Equal(2, result.RetriesRemaining);
        }
    }
}
