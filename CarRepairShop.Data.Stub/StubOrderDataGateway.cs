using System.Threading.Tasks;
using CarRepairShop.Domain.Models;

namespace CarRepairShop.Data.Stub
{
    internal sealed class StubOrderDataGateway : IOrderDataGateway
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

        public void Dispose()
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