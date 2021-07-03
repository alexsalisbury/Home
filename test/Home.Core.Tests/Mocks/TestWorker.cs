namespace Home.Core.Tests.Mocks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Home.Core.Workers;

    //public class TestWorker : HomeWorker
    //{
    //    public bool FailNext { get; set; }
    //    public bool RetryAll { get; set; }
    //    public bool RetryNext { get; set; }
    //    public bool CompleteNext { get; set; }

    //    public override async Task ExecuteWorkerAsync(CancellationToken stoppingToken)
    //    {
    //        if (FailNext)
    //        {
    //            throw new TaskCanceledException();
    //        }
    //        await Task.Delay(1);
    //    }

    //    public override async Task StartupAsync()
    //    {
    //    }
    //}
}
