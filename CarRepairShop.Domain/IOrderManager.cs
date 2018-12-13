using CarRepairShop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairShop.Domain
{
    public interface IOrderManager
    {
        Task AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetFreeOrdersAsync();
        Task<IEnumerable<Person>> GetMechanicsAsync();
        Task AssignMechanicAsync(Order order, Person mechanic);
        Task<IEnumerable<Order>> GetUnompleteOrdersAsync();
        Task CompleteOrderAsync(Order order);
        Task SetPriceAsync(Order order, double price);
    }
}