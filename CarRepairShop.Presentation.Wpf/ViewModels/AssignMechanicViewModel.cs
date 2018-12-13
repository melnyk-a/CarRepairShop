using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Presentation.Wpf.ToolTips;
using CarRepairShop.Presentation.Wpf.ViewModels.ViewModelFactory;
using CarRepairShop.Wpf.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class AssignMechanicViewModel : ViewModel, ITooltipMessageViewModel
    {
        private readonly IViewModelFactory factory;
        private readonly IOrderManager orderManager;
        private readonly ICollection<FreeOrderViewModel> orderViewModels = new ObservableCollection<FreeOrderViewModel>();

        private TooltipMessage tooltipMessage;

        public AssignMechanicViewModel(IOrderManager orderManager, IViewModelFactory factory)
        {
            this.factory = factory;
            this.orderManager = orderManager;
            LoadOrders();
        }

        public IEnumerable<FreeOrderViewModel> Orders => orderViewModels;

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }

        private async Task FillOrderViewModel(IEnumerable<PersonViewModel> mechanics)
        {
            var orders = await orderManager.GetFreeOrdersAsync();
            foreach (Order order in orders)
            {
                var viewModel = (FreeOrderViewModel)factory.CreateFreeOrderViewModel(order, mechanics);
                viewModel.MessageCreated += UpdateTooltipMessage;
                viewModel.MechanicAssigned += RemoveOrder;
                orderViewModels.Add(viewModel);
            }
        }

        private async Task<ObservableCollection<PersonViewModel>> GetMechanicsViewModels()
        {
            var mechanics = await orderManager.GetMechanicsAsync();
            var mechanicsViewModels = new ObservableCollection<PersonViewModel>();

            foreach (var mechanic in mechanics)
            {
                mechanicsViewModels.Add(new PersonViewModel(mechanic));
            }

            return mechanicsViewModels;
        }

        public async void LoadOrders()
        {
            orderViewModels.Clear();
            TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
            try
            {
                var mechanicsViewModels = await GetMechanicsViewModels();
                await FillOrderViewModel(mechanicsViewModels);
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
                    orderViewModel.MechanicAssigned -= RemoveOrder;
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