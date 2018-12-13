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
        Task SetPriceAsync(int orderId, double price);
        Task<IEnumerable<Order>> GetUnompleteOrdersAsync();
        Task CompleteOrderAsync(int orderId, DateTime finishDate);
    }
}