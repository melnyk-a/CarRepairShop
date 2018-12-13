using CarRepairShop.Data;
using CarRepairShop.Domain.Models;
using CarRepairShop.Domain.Validators.ValidationCommands;
using CarRepairShop.Domain.Validators.ValidationCommands.ValidationCommandFactories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CarRepairShop.Domain
{
    public sealed class OrderManager : IOrderManager
    {
        private readonly IOrderDataService dataService;
        private readonly IEnumerable<IValidationCommand> validationCommands;

        public OrderManager(IOrderDataService dataService, IValidationCommandFactory commandFactory)
        {
            this.dataService = dataService;
            validationCommands = commandFactory.CreateValidationCommands();
        }

        public async Task AddOrderAync(Order order)
        {
            order.StartDate = DateTime.Now;

            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    await dataGateway.AddOrder(order);
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task AssignMechanicAsync(Order order, Person mechanic)
        {
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    await dataGateway.AssignMechanicAsync(order.Id, mechanic.Id);
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task CompleteOrderAsync(Order order)
        {
            order.FinishDate = DateTime.Now;
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    await dataGateway.CompleteOrderAsync(order.Id, order.FinishDate);
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task<IEnumerable<Order>> GetFreeOrdersAsync()
        {
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    return await dataGateway.GetFreeOrdersAsync();
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task<IEnumerable<Person>> GetMechanicsAsync()
        {
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    return await dataGateway.GetMechanicsAsync();
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task<IEnumerable<Order>> GetOrdersAsync()
        {
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    return await dataGateway.GetOrdersAsync();
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task<IEnumerable<Order>> GetUncompleteOrdersAsync()
        {
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    return await dataGateway.GetUnompleteOrdersAsync();
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public async Task SetPriceAsync(Order order, double price)
        {
            using (IOrderDataGateway dataGateway = dataService.OpenDataGateway())
            {
                try
                {
                    await dataGateway.SetPriceAsync(order.Id, price);
                }
                catch (DataException e)
                {
                    throw new DomainException(e.Message);
                }
            }
        }

        public IEnumerable<string> ValidateProperty(string propertyName, string propertyValue)
        {
            IEnumerable<string> errors = new string[0];

            foreach (var validationCommand in validationCommands)
            {
                if (validationCommand.CanValidate(propertyName))
                {
                    errors = validationCommand.Validate(propertyValue);
                }
            }

            return errors;
        }
    }
}