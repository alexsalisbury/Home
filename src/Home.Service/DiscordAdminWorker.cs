namespace Home.Service
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Home.Core.DiscordBot.Interfaces.Clients;
    using Home.Core.DiscordBot.Clients;
    using Home.Core.DiscordBot.Interfaces.Services;
    using Home.Core.DiscordBot.Models.Settings;
    using Home.Core.DiscordBot.Services;
    using Home.Core.Models.Settings;
    using Home.Core.Workers;

    public class DiscordAdminWorker : HomeWorker
    {
        public static BotSettings ShyBotSettings { get; internal set; }
        private IMockableDiscordService DiscordManager;

        internal DiscordAdminWorker(int idx, ILogger<DiscordAdminWorker> logger) : base(nameof(DiscordAdminWorker), idx, logger)
        {
        }

        protected override async Task<bool> ExecuteWorkerAsync(CancellationToken stoppingToken)
        {
            // Default behavior is to soft write a message about once a minute.
            logger?.LogInformation("Execute: {name}:{idx}", this.WorkerType, this.Index);
            await InitializeAsync();

            while (!stoppingToken.IsCancellationRequested)
            {
                logger?.LogInformation("Worker running at: {time}", DateTimeOffset.UtcNow);
                await Task.Delay(60000, stoppingToken);
            }

            return true;
        }


        private async Task InitializeAsync()
        {
            await DiscordService.StartAsync(ShyBotSettings);

            while (!DiscordService.IsConnected())
            {
                await Task.Delay(100);
            }
            
            DiscordManager = DiscordService.Service;

            await Task.Delay(2000);
        }

    }

    //public class Worker : BackgroundService
    //{
    //    /// <summary>
    //    /// Global settings from Configuration.
    //    /// </summary>
    //    public static BotSettings ShyBotSettings { get; internal set; }
    //    public static AzureSettings AzureSettings { get; internal set; }
    //    private static IList<ServerInfo> EmptyServerSet = new List<ServerInfo>(new List<ServerInfo>());
    //    private IMockableDiscordService DiscordManager;

    //    public Worker()
    //    {
    //    }

    //    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    //    {
    //        await StartupAsync();
    //        await Task.Delay(1000 * (10 - (DateTime.UtcNow.Second%10)));
    //        Log.Information("Worker running at: {time}", DateTimeOffset.UtcNow);

    //        int c = DateTime.UtcNow.Minute / 10;
    //        while (!stoppingToken.IsCancellationRequested)
    //        {
    //            c = (c + 1) % 6;
    //            if (c == 0)
    //            {
    //                Log.Information("Worker running at: {time}", DateTimeOffset.UtcNow);
    //            }

    //            await Task.Delay(10000, stoppingToken);
    //        }
    //    }

    //    private async Task StartupAsync()
    //    {
    //        await Initialize();
    //        DiscordManager = DiscordService.Service;

    //        Log.Information("Running tasks");
    //        await QueueStartupTasksAsync();
    //    }

    //    private static async Task Initialize()
    //    {
    //        var cloud = new ShyCloudClient(AzureSettings);
    //        await DiscordService.StartAsync(ShyBotSettings);

    //        while (!DiscordService.IsConnected())
    //        {
    //            await Task.Delay(100);
    //        }

    //        await Task.Delay(2000);
    //        Log.Information("Go");
    //    }

    //    private async Task QueueStartupTasksAsync()
    //    {
    //        await Task.CompletedTask;
    //        //await DiscordManager.FetchConfigurationAsync();
    //        //await DiscordManager.SyncInfraAsync();
    //        //await DiscordManager.StartArchiveAsync();
    //    }

    //    private async Task QueueWaitingTasksAsync()
    //    {
    //        await Task.CompletedTask;
    //        // await DiscordManager.ArchiveAsync();
    //    }
    //}
}