namespace Home.Core.Tests.Workers
{
    using Xunit;
    using System.Threading.Tasks;
    using Home.Core.Services;
    using Home.Core.Tests.Mocks;
    using System.Threading;

    public class HomeWorker_Tests
    {
        [Fact]
        public void HomeWorkerCtor()
        {
            TestWorker tw = new TestWorker(0, null);
            Assert.NotNull(tw);
        }

        [Fact]
        public async Task HomeWorkerExecuteWorker()
        {
            CancellationTokenSource cts = new CancellationTokenSource();
            var tw = new TestWorker(0, null);
            await tw?.ExecuteDirectAsync(cts.Token);
            Assert.True(tw?.Executed ?? false);
        }
    }
}
