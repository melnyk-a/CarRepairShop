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

        public async Task AssignMechanicAsync(Order order, Person mechanic)
        {
            IOrderDataGateway dataGateway = dataService.OpenDataGateway();
            try
            {
                await dataGateway.AssignMechanicAsync(order.Id, mechanic.Id);
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

        public async Task<IEnumerable<Order>> GetFreeOrdersAsync()
        {
            IOrderDataGateway dataGateway = dataService.OpenDataGateway();
            try
            {
                return await dataGateway.GetFreeOrdersAsync();
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

        public async Task<IEnumerable<Person>> GetMechanicsAsync()
        {
            IOrderDataGateway dataGateway = dataService.OpenDataGateway();
            try
            {
                return await dataGateway.GetMechanicsAsync();
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