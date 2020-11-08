namespace Home.Core.Repositories
{
    using System.Data.Common;
    using System.Data.SqlClient;
    using Serilog;

    public abstract class BaseRepository
    {
        private DbConnection conn;
        protected string ConnectionString { get; }

        protected BaseRepository(string connstr)
        {
            ConnectionString = connstr;

            // If you ever see this eventcode in prod it's a problem. Either we can't load connstr OR we're logging it!
            if (string.IsNullOrWhiteSpace(connstr))
            {
                Log.Error("Connstr is blank or unset");
            }
            else
            {
                //Logger.LogTrace(LoggingEvents.BaseRepositoryConnstr, connstr);
            }
        }

        public DbConnection Connection
        {
            get
            {
                if (conn != null)
                {
                    return conn;
                }

                Log.Information("Connection created");
                return new SqlConnection(ConnectionString);
            }
            set
            {
                this.conn = value;
            }
        }
    }
}
