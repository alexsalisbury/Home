namespace Home.Core.DiscordBot.Tests.Repositories
{
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;
    using Moq;
    using Moq.Dapper;
    using Xunit;
    using Home.Core.DiscordBot.Models.Dtos;
    using Home.Core.DiscordBot.Repositories;

    public class ExplainRepository_Tests
    {
        [Fact]
        public void BasicRepoCtorTest()
        {
            var er = new ExplainRepository();
            Assert.NotNull(er);
        }

        [Fact]
        public async Task FetchTest()
        {
            var expected = new[] { new ExplainableDto() };
            var er = new ExplainRepository();

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ExplainableDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var result = await er.Fetch();
            Assert.NotNull(result);
            Assert.Equal(typeof(ExplainableDto), result.First().GetType());
        }
    }
}
