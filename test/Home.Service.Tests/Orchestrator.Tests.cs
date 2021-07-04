namespace Home.Service.Tests
{
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;
    using Home.Service;

    public class Orchestrator_Tests
    {
        [Fact]
        public void OrchestratorCtor()
        {
            var o = new Orchestrator(null, null);
            Assert.NotNull(o);
        }

        [Fact]
        public async Task OrchestratorInitialize()
        {
            var o = new Orchestrator(null, null);
            await o?.InitializeAsync();
        }

        [Fact]
        public async Task OrchestratorServiceLoop()
        {
            var cts = new CancellationTokenSource();
            var t = cts.Token;
            var o = new Orchestrator(null, null);
            await o?.InitializeAsync();
            await o.StartAsync(t);
            await o.StopAsync(t);
        }
    }

    public class DiscordAdminWorker_Tests
    {
        [Fact]
        public void DiscordAdminWorkerCtor()
        {
            var daw = new DiscordAdminWorker(0, null);
            Assert.NotNull(daw);
        }
    }
}
