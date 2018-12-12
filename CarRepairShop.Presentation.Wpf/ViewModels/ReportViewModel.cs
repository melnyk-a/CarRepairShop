using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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

            LoadOrders();
            
        }

        private async void LoadOrders()
        {
            TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
            try
            {
                var orders = await orderManager.GetOrders();
                foreach (Order order in orders)
                {
                    orderViewModel.Add(new DetailOrderViewModel(order));
                }
                TooltipMessage = new TooltipMessage("Loaded", MessageStatus.Successful);
            }
            catch(DomainException e)
            {
                TooltipMessage = new TooltipMessage(e.Message, MessageStatus.Error);
            }
        }

        public IEnumerable<DetailOrderViewModel> Orders  => orderViewModel;
           


        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }
    }
}