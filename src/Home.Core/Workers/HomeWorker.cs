namespace Home.Core.Workers
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public abstract class HomeWorker : BackgroundService
    {
        protected DateTime nextRun = DateTime.MinValue;
        protected TimeSpan tock = TimeSpan.FromMinutes(1);
        protected TimeSpan tick = TimeSpan.FromSeconds(1);
        public abstract Task StartupAsync();
        public abstract Task ExecuteWorkerAsync(CancellationToken stoppingToken);

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Log.Information("Worker starting at: {time}", DateTimeOffset.UtcNow);
            await StartupAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                if (nextRun < DateTime.UtcNow)
                {
                    await ExecuteSingle(stoppingToken);
                    nextRun = DateTime.UtcNow.Add(tock);
                }

                await Task.Delay(tick);
            }
        }

        protected async Task ExecuteSingle(CancellationToken stoppingToken)
        {
            Log.Information("Worker running at: {time}", DateTimeOffset.UtcNow);
            await ExecuteWorkerAsync(stoppingToken);
        }
    }
}
