using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Wpf.Attributes;
using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class FreeOrderViewModel: ViewModel, ITooltipMessageViewModel
    {
        private TooltipMessage tooltipMessage;
        private readonly Order order;
        private readonly IEnumerable<PersonViewModel> mechanics;
        private readonly PersonViewModel client;
        private readonly ICommand assignMechanicCommand;
        private readonly IOrderManager orderManager;

        private PersonViewModel selectedMechanic;

        public event EventHandler<TooltipMessageEventArgs> MessageCreated;
        public event EventHandler<OrderEvenArgs> MechanicAssigned;



        private void OnMessageCreated(TooltipMessage message)
        {
            MessageCreated?.Invoke(this, new TooltipMessageEventArgs(message));
        }

        private void OnMechanicAssigned()
        {
            MechanicAssigned?.Invoke(this, new OrderEvenArgs(order));
        }

        public FreeOrderViewModel(Order order, IEnumerable<PersonViewModel> mechanics, IOrderManager orderManager)
        {
            this.orderManager = orderManager;
            this.order = order;
            this.mechanics = mechanics;
            client = new PersonViewModel(order.Client.Person);
            assignMechanicCommand = new AsyncDelegateCommand(AssignMechanic, () => CanAssign);
        }

        public PersonViewModel Client => client;

        public string CarModel => order.Car.Model;

        public IEnumerable<PersonViewModel> Mechanics => mechanics;

        public PersonViewModel SelectedMechanic
        {
            get => selectedMechanic;
            set => SetProperty(ref selectedMechanic, value);
        }

        [DependsUponPropertyAttribute(nameof(SelectedMechanic))]
        public bool CanAssign => SelectedMechanic != null;

        [RaiseCanExecuteDependsUpon(nameof(CanAssign))]
        public ICommand AssignMechanicCommand => assignMechanicCommand;

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
                TooltipMessage = new TooltipMessage($"Mechanic {selectedMechanic.Name} for client {order.Client.Person.Name} {order.Client.Person.Surname} is successfully assigned.", MessageStatus.Successful);
                OnMechanicAssigned();
            }
            catch (DomainException ex)
            {
                TooltipMessage = new TooltipMessage(ex.Message, MessageStatus.Error);
            }
        }

        public Order Order => order;
    }
}