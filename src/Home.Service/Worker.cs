namespace Home.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Hosting;
    using Serilog;
    using Home.Core.Models.Settings;
    using Home.Service.Services;

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
            await InitializeAsync();
            await StartupAsync();
            await RunStartupTasksAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                Log.Information("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(10000, stoppingToken);
            }
        }

        private async Task InitializeAsync()
        {
            await DiscordService.StartAsync(ShyBotSettings);
        }

        private async Task StartupAsync()
        {

        }

        private async Task RunStartupTasksAsync()
        {
            //await ArchiveDiscordAsync();
        }
    }
}
