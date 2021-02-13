namespace Home.Core.Tests.Repositories
{
    using System.Data;
    using Xunit;
    using Home.Core.Tests.Mocks;

    public class BaseRepository_Tests
    {
        [Fact]
        public void BasicRepoTest()
        {
            var mr = new MockRepository();
            Assert.NotNull(mr);
            Assert.NotNull(mr.GetConnection());
            Assert.IsAssignableFrom(typeof(IDbConnection), mr.GetConnection());
        }

        [Fact]
        public void BasicRepoWithConnStrTest()
        {
            var mr = new MockRepository("Server=(localdb)\\mssqllocaldb;Database=FakeDb;Trusted_Connection=True;");
            Assert.NotNull(mr);
            Assert.NotNull(mr.GetConnection());
            Assert.IsAssignableFrom(typeof(IDbConnection), mr.GetConnection());
        }
    }
}
