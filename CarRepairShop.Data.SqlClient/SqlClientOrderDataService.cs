namespace CarRepairShop.Data.SqlClient
{
    public sealed class SqlClientOrderDataService : IOrderDataService
    {
        public IOrderDataGateway OpenDataGateway()
        {
            return new SqlClientOrderDataGateway();
        }
    }
}