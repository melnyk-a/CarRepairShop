using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class AssignMechanicViewModel : ViewModel, ITooltipMessageViewModel
    {
        private readonly IOrderManager orderManager;
        private readonly ICollection<FreeOrderViewModel> orderViewModels = new ObservableCollection<FreeOrderViewModel>();
        private TooltipMessage tooltipMessage;

        public AssignMechanicViewModel(IOrderManager orderManager)
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
                var orders = await orderManager.GetFreeOrdersAsync();
                var mechanics = await orderManager.GetMechanicsAsync();

                var mechanicsViewModels = new ObservableCollection<PersonViewModel>();

                foreach(var mechanic in mechanics)
                {
                    mechanicsViewModels.Add(new PersonViewModel(mechanic));
                }

                foreach (Order order in orders)
                {
                    var viewModel = new FreeOrderViewModel(order, mechanicsViewModels, orderManager);
                    viewModel.MessageCreated += (sender, e) =>
                      {
                          TooltipMessage = e.Message;
                      };

                    viewModel.MechanicAssigned += (sender, e) =>
                         {
                             foreach(var orderViewModel in orderViewModels)
                             {
                                 if(orderViewModel.Order.Id == e.Order.Id)
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

        public IEnumerable<FreeOrderViewModel> Orders => orderViewModels;
    }
}