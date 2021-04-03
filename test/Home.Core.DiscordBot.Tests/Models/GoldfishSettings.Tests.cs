namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Settings;

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
