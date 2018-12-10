using CarRepairShop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CarRepairShop.Data
{
    public interface IOrderDataGateway : IDisposable
    {
        bool IsClientExist(Person client, string phone);
        bool IsCarExist(Car car);
        Task AddClient(Person client, string phone);
        Task AddCar(Car car);
        Task AddOrder(Order order);
    }
}