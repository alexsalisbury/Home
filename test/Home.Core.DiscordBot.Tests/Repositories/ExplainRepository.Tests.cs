namespace Home.Core.DiscordBot.Tests.Repositories
{
    using System;
    using System.Data;
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

            var result = await er.FetchAsync();
            Assert.NotNull(result);
            Assert.Equal(typeof(ExplainableDto), result.First().GetType());
        }

        [Fact]
        public async Task FetchDefaultsTest()
        {
            var er = new ExplainRepository();
            var expected = ExplainRepository.DefaultCommands.ToArray();

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ExplainableDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var result = await er.FetchAsync();
            Assert.NotNull(result);
            Assert.Equal(typeof(ExplainableDto), result.First().GetType());
        }

        [Fact]
        public async Task FetchDefaultSingleTest()
        {
            var id = 21002;
            var er = new ExplainRepository();
            var expected = ExplainRepository.DefaultCommands.Where(dc => dc.ShyId == id);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ExplainableDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var result = await er.FetchAsync(id);
            Assert.NotNull(result);
            Assert.Equal(typeof(ExplainableDto), result.GetType());
        }

        [Fact]
        public async Task FetchMissingSingleTest()
        {
            var id = 789;
            var er = new ExplainRepository();
            var expected = ExplainRepository.DefaultCommands.Where(dc => dc.ShyId == id);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ExplainableDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var result = await er.FetchAsync(id);
            Assert.Null(result);
        }

        [Fact]
        public async Task FetchMissingSingleUlongTest()
        {
            ulong id = 123;
            var er = new ExplainRepository();
            var expected = ExplainRepository.DefaultCommands.Where(dc => dc.ShyId == (int)id);

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<ExplainableDto>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            var result = await er.FetchAsync(id);
            Assert.Null(result);
        }
    }
}
