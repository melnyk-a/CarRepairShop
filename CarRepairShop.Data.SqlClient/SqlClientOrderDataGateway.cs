using CarRepairShop.Domain.Models;
using System.Threading.Tasks;

namespace CarRepairShop.Data.SqlClient
{
    internal sealed class SqlClientOrderDataGateway : DisposableObject, IOrderDataGateway
    {
        public Task AddCar(Car car)
        {
            throw new System.NotImplementedException();
        }

        public Task AddClient(Person client, string phone)
        {
            throw new System.NotImplementedException();
        }

        public Task AddOrder(Order order)
        {
            throw new System.NotImplementedException();
        }

        protected override void Dispose(bool disposing)
        {
            throw new System.NotImplementedException();
        }

        public bool IsCarExist(Car car)
        {
            throw new System.NotImplementedException();
        }

        public bool IsClientExist(Person client, string phone)
        {
            throw new System.NotImplementedException();
        }
    }
}