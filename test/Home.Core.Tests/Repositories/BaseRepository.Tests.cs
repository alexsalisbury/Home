namespace Home.Core.Tests.Repositories
{
    using System.Data;
    using System.Linq;
    using Xunit;
    using Home.Test.Core.Mocks;
    using System.Threading.Tasks;
    using Moq;
    using System.Data.Common;
    using Moq.Dapper;
    using Dapper;

    public class BaseRepository_Tests
    {
        private static string fakeconnstr = "Server=(localdb)\\mssqllocaldb;Database=FakeDb;Trusted_Connection=True;";
        [Fact]
        public void BasicRepoTest()
        {
            var mr = new MockHomeRepository();
            Assert.NotNull(mr);
            Assert.NotNull(mr.GetConnection());
            Assert.IsAssignableFrom(typeof(IDbConnection), mr.GetConnection());
        }

        [Fact]
        public void BasicRepoWithConnStrTest()
        {
            var mr = new MockHomeRepository(fakeconnstr);
            Assert.NotNull(mr);
            Assert.NotNull(mr.GetConnection());
            Assert.IsAssignableFrom(typeof(IDbConnection), mr.GetConnection());
        }

        [Fact]
        public void BasicResetTest()
        {
            var mr = new MockHomeRepository();
            var conn = mr.GetConnection();
            mr.SetConnection(null);
            var conn2 = mr.GetConnection();
            Assert.IsAssignableFrom(typeof(IDbConnection), conn2);
            Assert.NotSame(conn, conn2);
        }

        [Fact]
        public void BasicSetTest()
        {
            var mr = new MockHomeRepository();
            var conn = mr.GetConnection();
            mr.SetConnection(null);
            var conn2 = mr.GetConnection();
            mr.SetConnection(conn);
            Assert.Same(conn, mr.GetConnection());
            Assert.NotSame(conn, conn2);
        }

        [Fact]
        public async Task FetchTest()
        {
            var procName = "example";
            var expected = new[] { procName };
            var mr = new MockHomeRepository();

            var connection = new Mock<DbConnection>();
            connection.SetupDapperAsync(c => c.QueryAsync<string>(It.IsAny<string>(), null, null, null, null)).ReturnsAsync(expected);

            mr.SetConnection(connection.Object);
            var result = await mr.FetchAsync(procName);
            Assert.NotNull(result);
            Assert.Equal(typeof(string), result.First().GetType());
            Assert.Equal(procName, result.First());
        }
    }
}
