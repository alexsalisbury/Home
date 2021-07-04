namespace Home.Core.Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;

    public abstract class HomeWorker // No longer a BackgroundService
    {
        public string WorkerType { get; }
        protected int Index { get; }
        protected readonly ILogger logger;

        protected HomeWorker(string workerType, int idx, ILogger<HomeWorker> logger)
        {
            this.Index = idx;
            this.logger = logger;
            this.WorkerType = workerType;
        }

        internal protected abstract Task<bool> ExecuteWorkerAsync(CancellationToken stoppingToken);

        internal async virtual Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await ExecuteWorkerAsync(stoppingToken);
        }
    }
}
