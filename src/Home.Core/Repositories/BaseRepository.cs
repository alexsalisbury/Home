namespace Home.Core.Repositories
{
    using System.Data;
    using System.Data.SqlClient;

    public abstract class BaseRepository
    {
        private IDbConnection conn;
        protected string ConnectionString { get; }

        protected BaseRepository(string connstr)
        {
            this.ConnectionString = connstr;
        }

        public IDbConnection Connection
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
    }
}
