using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Presentation.Wpf.ToolTips;
using CarRepairShop.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class CompleteOrderViewModel : ViewModel, ITooltipMessageViewModel
    {
        private readonly IOrderManager orderManager;
        private readonly ICollection<UncompleteOrderViewModel> orderViewModels = new ObservableCollection<UncompleteOrderViewModel>();

        private TooltipMessage tooltipMessage;

        public CompleteOrderViewModel(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        public IEnumerable<UncompleteOrderViewModel> Orders => orderViewModels;

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }

        private async void FillOrderViewModel()
        {
            var orders = await orderManager.GetUncompleteOrdersAsync();

            foreach (Order order in orders)
            {
                var viewModel = new UncompleteOrderViewModel(order, orderManager);
                viewModel.MessageCreated += UpdateTooltipMessage;
                viewModel.OrderCompleted += RemoveOrder;
                orderViewModels.Add(viewModel);
            }
        }

        public void LoadOrders()
        {
            orderViewModels.Clear();
            TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
            try
            {
                FillOrderViewModel();
                TooltipMessage = new TooltipMessage("Loaded", MessageStatus.Successful);
            }
            catch (DomainException e)
            {
                TooltipMessage = new TooltipMessage(e.Message, MessageStatus.Error);
            }
        }

        private void RemoveOrder(object sender, OrderEvenArgs orderEvenArgs)
        {
            foreach (var orderViewModel in orderViewModels)
            {
                if (orderViewModel.Order.Id == orderEvenArgs.Order.Id)
                {
                    orderViewModel.MessageCreated -= UpdateTooltipMessage;
                    orderViewModel.OrderCompleted -= RemoveOrder;
                    orderViewModels.Remove(orderViewModel);
                    break;
                }
            }
        }

        private void UpdateTooltipMessage(object sender, TooltipMessageEventArgs messageEventArgs)
        {
            TooltipMessage = messageEventArgs.Message;
        }
    }
}