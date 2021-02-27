namespace Home.Core.Tests.Mocks
{
    using System.Data;
    using System.Data.Common;
    using Home.Core.Repositories;

    public class MockRepository : BaseRepository
    {
        public MockRepository() : base("")
        {
        }

        public MockRepository(string connstr) : base(connstr)
        {
        }

        public IDbConnection GetConnection()
        {
            return this.Connection;
        }

        public void SetConnection(DbConnection conn)
        {
            this.Connection = conn;
        }
    }
}