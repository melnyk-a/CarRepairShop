using CarRepairShop.Data;
using CarRepairShop.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairShop.Domain
{
    public sealed class OrderManager : IOrderManager
    {
        private readonly IOrderDataService dataService;

        public OrderManager(IOrderDataService dataService)
        {
            this.dataService = dataService;
        }

        public async Task AddOrder(Order order)
        {
            order.StartDate = DateTime.Now;

            IOrderDataGateway dataGateway = dataService.OpenDataGateway();
            try
            {
                await dataGateway.AddOrder(order);
            }
            catch(DataException ex)
            {
                throw new DomainException(ex.Message);
            }
            finally
            {
                dataGateway.Dispose();
            }
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            IOrderDataGateway dataGateway = dataService.OpenDataGateway();
            try
            {
                return await dataGateway.GetOrdersAsync();
            }
            catch (DataException ex)
            {
                throw new DomainException(ex.Message);
            }
            finally
            {
                dataGateway.Dispose();
            }
        }
    }
}