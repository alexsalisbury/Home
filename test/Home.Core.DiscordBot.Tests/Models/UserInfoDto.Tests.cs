namespace Home.Core.DiscordBot.Tests.Models
{
    using Xunit;
    using Home.Core.DiscordBot.Models.Dtos;

    public class UserInfoDto_Tests
    {
        [Fact]
        public void BasicUserInfoDto()
        {
            var udto = new UserInfoDto();
            Assert.NotNull(udto);
        }
    }
}
