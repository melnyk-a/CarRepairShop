using CarRepairShop.Data;
using CarRepairShop.Domain.Models;
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
    }
}