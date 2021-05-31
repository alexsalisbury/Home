namespace Home.Core.Tests
{
    using System.Threading.Tasks;
    using Xunit;
    using Home.Core.Tests.Mocks;

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
            Assert.True(result.IsStageComplete);
            Assert.False(result.IsCommandComplete);
            Assert.Equal(3, result.RetriesRemaining);
        }

        [Fact]
        public async Task ExecuteTestCompleteCommand()
        {
            var tc = new TestCommand();
            tc.CompleteNext = true;
            var result = await tc.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsStageComplete);
            Assert.True(result.IsCommandComplete);
            Assert.Equal(3, result.RetriesRemaining);
        }

        [Fact]
        public async Task FailTestCommand()
        {
            var tc = new TestCommand();
            tc.FailNext = true;
            var result = await tc.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsStageComplete);
            Assert.False(result.IsCommandComplete);
            Assert.Equal(3, result.RetriesRemaining);
        }

        [Fact]
        public async Task RetryAndCompleteTestCommand()
        {
            var tc = new TestCommand();
            tc.RetryNext = true;
            var retryResult = await tc.ExecuteCommandStageAsync();
            Assert.Equal(2, retryResult.RetriesRemaining);
            Assert.False(retryResult.IsStageComplete);

            var tc2 = new TestCommand(retryResult);
            tc2.CompleteNext = true;
            var result = await tc2.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsStageComplete);
            Assert.True(result.IsCommandComplete);
            Assert.Equal(2, result.RetriesRemaining);
        }

        [Fact]
        public async Task FailOnRetryTestCommand()
        {
            var tc = new TestCommand();
            tc.RetryNext = true;
            var retryResult = await tc.ExecuteCommandStageAsync();
            Assert.Equal(2, retryResult.RetriesRemaining);
            Assert.False(retryResult.IsStageComplete);

            var tc2 = new TestCommand(retryResult);
            tc2.FailNext = true;
            var result = await tc2.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsStageComplete);
            Assert.False(result.IsCommandComplete);
            Assert.Equal(2, result.RetriesRemaining);
        }

        [Fact]
        public async Task RetryLoopTestCommand()
        {
            //todo extract helper funcs?
            var tc = new TestCommand();
            tc.RetryAll = true;
            var retry1 = await tc.ExecuteCommandStageAsync();
            Assert.Equal(2, retry1.RetriesRemaining);
            Assert.False(retry1.IsStageComplete);
            var tc2 = new TestCommand(retry1);
            tc2.RetryAll = true;
            var retry2 = await tc2.ExecuteCommandStageAsync();
            Assert.Equal(1, retry2.RetriesRemaining);
            Assert.False(retry2.IsStageComplete);
            var tc3 = new TestCommand(retry2);
            tc3.RetryAll = true;
            var retry3 = await tc3.ExecuteCommandStageAsync();
            Assert.Equal(0, retry3.RetriesRemaining);
            Assert.False(retry3.IsStageComplete);
            var tc4 = new TestCommand(retry3);
            var result = await tc4.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.False(result.IsStageComplete);
            Assert.False(result.IsCommandComplete);
          //  Assert.Equal(0, result.RetriesRemaining);
        }

        [Fact]
        public async Task FailToCompleteTestCommand()
        {
            var tc = new TestCommand();
            tc.FailNext = true;
            var failResult = await tc.ExecuteCommandStageAsync();
            Assert.Equal(3, failResult.RetriesRemaining);
            Assert.True(failResult.IsStageComplete);
            var tc2 = new TestCommand(failResult);
            tc2.CompleteNext = true;
            var result = await tc2.ExecuteCommandStageAsync();
            Assert.NotNull(result);
            Assert.True(result.IsStageComplete);
            Assert.True(result.IsCommandComplete);
            Assert.Equal(3, result.RetriesRemaining);
        }
    }
}
