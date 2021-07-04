namespace Home.Core.Tests.Mocks
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Home.Core.Workers;

    public class TestWorker : HomeWorker
    {
        public TestWorker(int idx, ILogger<TestWorker> logger) : base(nameof(TestWorker), idx, logger)
        {
        }

        public bool FailNext { get; set; }
        internal int Index => base.Index;
        internal bool Executed { get; private set; } = false;

        //    public bool RetryAll { get; set; }
        //    public bool RetryNext { get; set; }
        //    public bool CompleteNext { get; set; }

        internal async Task<bool> ExecuteDirectAsync(CancellationToken stoppingToken)
        {
            return await ExecuteWorkerAsync(stoppingToken);
        }

        protected override async Task<bool> ExecuteWorkerAsync(CancellationToken stoppingToken)
        {
            if (FailNext)
            {
                throw new TaskCanceledException();
            }

            this.Executed = true;
            await Task.CompletedTask;
            return this.Executed;
        }
    }
}
