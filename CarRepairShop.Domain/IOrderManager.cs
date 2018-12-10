using CarRepairShop.Domain.Models;
using System.Threading.Tasks;

namespace CarRepairShop.Domain
{
    public interface IOrderManager
    {
        Task AddOrder(Order order);
    }
}