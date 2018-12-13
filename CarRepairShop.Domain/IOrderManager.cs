using CarRepairShop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairShop.Domain
{
    public interface IOrderManager
    {
        Task AddOrderAync(Order order);
        Task AssignMechanicAsync(Order order, Person mechanic);
        Task CompleteOrderAsync(Order order);
        Task<IEnumerable<Order>> GetFreeOrdersAsync();
        Task<IEnumerable<Person>> GetMechanicsAsync();
        Task<IEnumerable<Order>> GetOrdersAsync();
        Task<IEnumerable<Order>> GetUnompleteOrdersAsync();
        Task SetPriceAsync(Order order, double price);
        IEnumerable<string> ValidateProperty(string propertyName, string propertyValue);
    }
}