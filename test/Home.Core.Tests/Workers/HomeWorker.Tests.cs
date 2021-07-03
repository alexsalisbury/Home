namespace Home.Core.Tests.Workers
{
    using Xunit;
    using System.Threading.Tasks;
    using Home.Core.Services;
    using Home.Core.Tests.Mocks;
    using System.Threading;

    public class HomeWorker_Tests
    {
        //[Fact]
        //public void TestHomeWorker()
        //{
        //    TestWorker tw = new TestWorker();
        //    Assert.NotNull(tw);
        //}

        //[Fact]
        //public async Task ExecHomeWorker()
        //{
        //    CancellationTokenSource cts = new CancellationTokenSource();
        //    TestWorker tw = new TestWorker();
        //    await tw.StartAsync(cts.Token);
        //    await Task.Delay(1);
        //    await tw.StopAsync(cts.Token);
        //}

        //[Fact]
        //public async Task CancelHomeWorker()
        //{
        //    CancellationTokenSource cts = new CancellationTokenSource();
        //    TestWorker tw = new TestWorker();
        //    await tw.StartAsync(cts.Token);
        //    cts.Cancel();
        //    await tw.StopAsync(cts.Token);
        //}
    }
}
