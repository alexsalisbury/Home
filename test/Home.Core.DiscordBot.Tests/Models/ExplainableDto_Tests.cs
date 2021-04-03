namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Dtos;

    public class ExplainableDto_Tests
    {
        [Fact]
        public void BasicExplainableDto()
        {
            var edto = new ExplainableDto();
            Assert.NotNull(edto);
        }
    }
}
