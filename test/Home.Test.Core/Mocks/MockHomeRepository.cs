namespace Home.Test.Core.Mocks
{
    using System.Data;
    using System.Data.Common;
    using System.Linq;
    using System.Threading.Tasks;
    using Home.Core.Repositories;

    public class MockHomeRepository : BaseRepository
    {
        public MockHomeRepository() : base("")
        {
        }

        public MockHomeRepository(string connstr) : base(connstr)
        {
        }

        public IDbConnection GetConnection()
        {
            return this.Connection;
        }

        public void SetConnection(IDbConnection conn)
        {
            this.Connection = conn;
        }

        public async Task<IQueryable<string>> FetchAsync(string procName)
        {
            return await base.FetchAsync<string>(procName);
        }
    }
}