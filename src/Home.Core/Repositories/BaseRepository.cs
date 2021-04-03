namespace Home.Core.Repositories
{
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Threading.Tasks;
    using Dapper;

    public abstract class BaseRepository
    {
        private IDbConnection conn;
        protected string ConnectionString { get; }

        protected BaseRepository(string connstr)
        {
            this.ConnectionString = connstr;
        }

        protected BaseRepository(IDbConnection connection)
        {
            conn = connection;
        }

        protected IDbConnection Connection
        {
            get
            {
                if (conn != null)
                {
                    return conn;
                }

                conn = new SqlConnection(ConnectionString);
                return conn;
            }
            set
            {
                this.conn = value;
            }
        }

        protected async Task EnsureAsync(string procName, object param = null)
        {
            using (var dbConnection = Connection)
            {
                dbConnection.Open();
                await dbConnection.ExecuteAsync(procName, param, commandType: CommandType.StoredProcedure);
               // Log.Information($"Ensure completed: {procName}:{param}");
            }
        }

        protected async Task<IQueryable<T>> FetchAsync<T>(string fetchProcName)
        {
            using (var dbConnection = Connection)
            {
                var result = await dbConnection.QueryAsync<T>(fetchProcName, commandType: CommandType.StoredProcedure);
                return result.AsQueryable();
            }
        }
    }
}
