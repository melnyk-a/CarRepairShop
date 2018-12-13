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

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }

        public async void LoadOrders()
        {
            orderViewModels.Clear();
            TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
            try
            {
                var orders = await orderManager.GetUnompleteOrdersAsync();

                foreach (Order order in orders)
                {
                    var viewModel = new UncompleteOrderViewModel(order, orderManager);
                    viewModel.MessageCreated += (sender, e) =>
                    {
                        TooltipMessage = e.Message;
                    };

                    viewModel.OrderCompleted += (sender, e) =>
                    {
                        foreach (var orderViewModel in orderViewModels)
                        {
                            if (orderViewModel.Order.Id == e.Order.Id)
                            {
                                orderViewModels.Remove(orderViewModel);
                                break;
                            }
                        }
                    };
                    orderViewModels.Add(viewModel);
                }
                TooltipMessage = new TooltipMessage("Loaded", MessageStatus.Successful);
            }
            catch (DomainException e)
            {
                TooltipMessage = new TooltipMessage(e.Message, MessageStatus.Error);
            }
        }

        public IEnumerable<UncompleteOrderViewModel> Orders => orderViewModels;
    }
}