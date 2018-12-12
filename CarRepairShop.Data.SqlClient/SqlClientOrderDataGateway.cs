using CarRepairShop.Domain.Models;
using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Configuration;

namespace CarRepairShop.Data.SqlClient
{
    internal sealed class SqlClientOrderDataGateway : DisposableObject, IOrderDataGateway
    {
        private readonly Lazy<SqlConnection> connection;

        public SqlClientOrderDataGateway()
        {
            connection = new Lazy<SqlConnection>(() => TryOpenConnection());
        }

        private SqlConnection Connection => connection.Value;

        private SqlConnection TryOpenConnection()
        {
            try
            {
                string connectionString = ConfigurationManager
                    .ConnectionStrings["SqlConnection"]
                    .ConnectionString;

                var connection = new SqlConnection(connectionString);
                connection.Open();

                return connection;
            }
            catch (SqlException ex)
            {
                throw new DataException(ex.Message);
            }
        }

        public async Task AddOrder(Order order)
        {
            throw new NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            if (connection.IsValueCreated)
            {
                connection.Value.Close();
            }
        }
    }
}