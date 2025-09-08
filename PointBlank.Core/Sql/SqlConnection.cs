using Npgsql;
using System.Runtime.Remoting.Contexts;

namespace PointBlank.Core.Sql
{
    [Synchronization]
    public class SqlConnection
    {
        private static SqlConnection sql = new SqlConnection();
        protected NpgsqlConnectionStringBuilder connBuilder;

        static SqlConnection()
        {

        }

        public SqlConnection()
        {
            connBuilder = new NpgsqlConnectionStringBuilder
            {
                Database = Config.dbName,
                Host = Config.dbHost,
                Username = Config.dbUser,
                Password = Config.dbPass,
                Port = Config.dbPort
            };
        }

        public static SqlConnection getInstance()
        {
            return sql;
        }

        public NpgsqlConnection conn()
        {
            return new NpgsqlConnection(connBuilder.ConnectionString);
        }
    }
}