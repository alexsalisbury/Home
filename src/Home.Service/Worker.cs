namespace Home.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Home.Core.Models.Settings;
    using Microsoft.Extensions.Hosting;
    using Serilog;

    public class Worker : BackgroundService
    {
        /// <summary>
        /// Global settings from Configuration.
        /// </summary>
        public static BotSettings ShyBotSettings { get; internal set; }
        public static AzureSettings AzureSettings { get; internal set; }

        public Worker()
        {
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Initialize();

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task Initialize()
        {

        }
    }
}
