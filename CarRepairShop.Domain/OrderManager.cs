using System;
using System.Threading.Tasks;
using CarRepairShop.Data;
using CarRepairShop.Domain.Models;

namespace CarRepairShop.Domain
{
    internal sealed class OrderManager : IOrderManager
    {
        private readonly IOrderDataService dataService;

        public OrderManager(IOrderDataService dataService)
        {
            this.dataService = dataService;
        }

        public Task AddOrder(Order order)
        {
            throw new NotImplementedException();
        }
    }
}