using CarRepairShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairShop.Data
{
    public interface IOrderDataGateway : IDisposable
    {
        Task AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetFreeOrdersAsync();
        Task<IEnumerable<Person>> GetMechanicsAsync();
        Task AssignMechanicAsync(int orderId, int mechanicId);
    }
}