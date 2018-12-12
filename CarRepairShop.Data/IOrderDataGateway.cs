using CarRepairShop.Domain.Models;
using System;
using System.Threading.Tasks;

namespace CarRepairShop.Data
{
    public interface IOrderDataGateway : IDisposable
    {
        Task AddOrder(Order order);
    }
}