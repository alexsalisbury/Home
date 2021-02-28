namespace Home.Core.Tests.Repositories
{
    using System.Data;
    using Xunit;
    using Home.Test.Core.Mocks;

    public class BaseRepository_Tests
    {
        private static string fakeconnstr = "Server=(localdb)\\mssqllocaldb;Database=FakeDb;Trusted_Connection=True;";
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
            var mr = new MockRepository(fakeconnstr);
            Assert.NotNull(mr);
            Assert.NotNull(mr.GetConnection());
            Assert.IsAssignableFrom(typeof(IDbConnection), mr.GetConnection());
        }

        [Fact]
        public void BasicResetTest()
        {
            var mr = new MockRepository();
            var conn = mr.GetConnection();
            mr.Connection = null;
            var conn2 = mr.GetConnection();
            Assert.IsAssignableFrom(typeof(IDbConnection), conn2);
            Assert.NotSame(conn, conn2);
        }

        [Fact]
        public void BasicSetTest()
        {
            var mr = new MockRepository();
            var conn = mr.GetConnection();
            mr.Connection = null;
            var conn2 = mr.GetConnection();
            mr.Connection = conn;
            Assert.Same(conn, mr.GetConnection());
            Assert.NotSame(conn, conn2);
        }
    }
}
