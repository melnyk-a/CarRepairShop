using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Presentation.Wpf.ToolTips;
using CarRepairShop.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class ReportViewModel : ViewModel, ITooltipMessageViewModel
    {
        private readonly IOrderManager orderManager;
        private readonly ICollection<DetailOrderViewModel> orderViewModel = new ObservableCollection<DetailOrderViewModel>();

        private TooltipMessage tooltipMessage;

        public ReportViewModel(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        public IEnumerable<DetailOrderViewModel> Orders => orderViewModel;

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }

        private async Task FillOrderViewModel()
        {
            var orders = await orderManager.GetOrdersAsync();
            foreach (Order order in orders)
            {
                orderViewModel.Add(new DetailOrderViewModel(order));
            }
        }

        public async void LoadOrders()
        {
            orderViewModel.Clear();
            TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
            try
            {
                await FillOrderViewModel();
                TooltipMessage = new TooltipMessage("Loaded", MessageStatus.Successful);
            }
            catch (DomainException e)
            {
                TooltipMessage = new TooltipMessage(e.Message, MessageStatus.Error);
            }
        }
    }
}