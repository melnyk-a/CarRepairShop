using CarRepairShop.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairShop.Domain
{
    public interface IOrderManager
    {
        Task AddOrder(Order order);
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetFreeOrdersAsync();
        Task<IEnumerable<Person>> GetMechanicsAsync();
        Task AssignMechanicAsync(Order order, Person mechanic);
    }
}