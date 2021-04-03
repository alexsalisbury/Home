namespace Home.Core.DiscordBot.Tests.Models
{
    using System.Collections.Generic;
    using Xunit;
    using Home.Core.DiscordBot.Models;
    using Home.Core.DiscordBot.Models.Settings;

    public class DiscordChannel_Tests
    {
        [Fact]
        public void BasicDiscordChannel()
        {
            var dc = new DiscordChannel(string.Empty, null);
            Assert.NotNull(dc);
        }
    }

    public class Server_Tests
    {
        [Fact]
        public void BasicServerCtor()
        {
            var si = new ServerInfo()
            {
                Channels = new List<ChannelSettings>(),
                Codeword = "DiscordChannel_Tests",
                DiscordToken = "",
                ServerId = 0
            };

            var ls = new ListenerServer(si);
            Assert.NotNull(ls);
        }
    }

    public class GoldfishSettings_Tests
    {
        [Fact]
        public void BasicGoldfishSettings()
        {
            var gs = new GoldfishSettings();
            Assert.NotNull(gs);
        }
    }
}
