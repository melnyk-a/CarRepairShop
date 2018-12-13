using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Presentation.Wpf.ToolTips;
using CarRepairShop.Presentation.Wpf.ViewModels.ViewModelFactory;
using CarRepairShop.Wpf.Attributes;
using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class FreeOrderViewModel : ViewModel, ITooltipMessageViewModel
    {
        private readonly ICommand assignMechanicCommand;
        private readonly PersonViewModel client;
        private readonly IViewModelFactory factory;
        private readonly IEnumerable<PersonViewModel> mechanics;
        private readonly Order order;
        private readonly IOrderManager orderManager;

        private PersonViewModel selectedMechanic;
        private TooltipMessage tooltipMessage;

        public event EventHandler<TooltipMessageEventArgs> MessageCreated;
        public event EventHandler<OrderEvenArgs> MechanicAssigned;

        public FreeOrderViewModel(Order order, IEnumerable<PersonViewModel> mechanics, IOrderManager orderManager, IViewModelFactory factory)
        {
            this.factory = factory;
            this.orderManager = orderManager;
            this.order = order;
            this.mechanics = mechanics;
            client = (PersonViewModel)factory.CreatePersonViewModel(order.Client.Person);

            assignMechanicCommand = new AsyncDelegateCommand(AssignMechanic, () => CanAssign);
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAssign))]
        public ICommand AssignMechanicCommand => assignMechanicCommand;

        [DependsUponPropertyAttribute(nameof(SelectedMechanic))]
        public bool CanAssign => SelectedMechanic != null;

        public string CarModel => order.Car.Model;

        public PersonViewModel Client => client;

        public IEnumerable<PersonViewModel> Mechanics => mechanics;

        public Order Order => order;

        public PersonViewModel SelectedMechanic
        {
            get => selectedMechanic;
            set => SetProperty(ref selectedMechanic, value);
        }

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set
            {
                tooltipMessage = value;
                OnMessageCreated(tooltipMessage);
            }
        }

        private async Task AssignMechanic()
        {
            TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
            try
            {
                TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
                await orderManager.AssignMechanicAsync(order, selectedMechanic.Person);
                TooltipMessage = new TooltipMessage($"Mechanic {selectedMechanic.Name} for client {client.Name} is successfully assigned.", MessageStatus.Successful);
                OnMechanicAssigned();
            }
            catch (DomainException ex)
            {
                TooltipMessage = new TooltipMessage(ex.Message, MessageStatus.Error);
            }
        }

        private void OnMechanicAssigned()
        {
            MechanicAssigned?.Invoke(this, new OrderEvenArgs(order));

        }
        private void OnMessageCreated(TooltipMessage message)
        {
            MessageCreated?.Invoke(this, new TooltipMessageEventArgs(message));
        }
    }
}