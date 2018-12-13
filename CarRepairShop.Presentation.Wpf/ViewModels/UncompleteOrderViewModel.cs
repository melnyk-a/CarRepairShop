using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Domain.Validators;
using CarRepairShop.Presentation.Wpf.ToolTips;
using CarRepairShop.Wpf.Attributes;
using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class UncompleteOrderViewModel : ValidatableViewModel, ITooltipMessageViewModel
    {
        private TooltipMessage tooltipMessage;
        private readonly Order order;
        private readonly PersonViewModel client;
        private readonly ICommand setPriceCommand;
        private readonly ICommand completeOrderCommand;
        private string currentPrice;
        private readonly IOrderManager orderManager;

        public event EventHandler<TooltipMessageEventArgs> MessageCreated;
        public event EventHandler<OrderEvenArgs> OrderCompleted;

        private string enteredPrice;

        private void OnMessageCreated(TooltipMessage message)
        {
            MessageCreated?.Invoke(this, new TooltipMessageEventArgs(message));
        }

        private void OnOrderCompleted()
        {
            OrderCompleted?.Invoke(this, new OrderEvenArgs(order));
        }

        public UncompleteOrderViewModel(Order order, IOrderManager orderManager)
        {
            this.orderManager = orderManager;
            this.order = order;
            client = new PersonViewModel(order.Client.Person);

            setPriceCommand = new AsyncDelegateCommand(SetPrice, () => CanSetPrice);
            completeOrderCommand = new AsyncDelegateCommand(CompleteOrder);
            currentPrice = order.Price.ToString();
        }

        public PersonViewModel Client => client;

        public string CarModel => order.Car.Model;


        [DependsUponProperty(nameof(EnteredPrice))]
        public bool CanSetPrice => !HasErrors;

        [RaiseCanExecuteDependsUpon(nameof(CanSetPrice))]
        public ICommand SetPriceCommand => setPriceCommand;

        public ICommand CompleteOrderCommand => completeOrderCommand;

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set
            {
                tooltipMessage = value;
                OnMessageCreated(tooltipMessage);
            }
        }
        public string CurrentPrice
        {
            get => currentPrice;
            set => SetProperty(ref currentPrice, value);
        }

        public string EnteredPrice
        {
            get => enteredPrice;
            set => SetProperty(ref enteredPrice, value);
        }

        private async Task SetPrice()
        {
            Validate();
            if (!HasErrors)
            {
                TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
                try
                {
                    await orderManager.SetPriceAsync(order, double.Parse(enteredPrice));
                    TooltipMessage = new TooltipMessage($"Price for client {client.Name} is successfully set.", MessageStatus.Successful);
                    CurrentPrice = EnteredPrice;
                }
                catch (DomainException ex)
                {
                    TooltipMessage = new TooltipMessage(ex.Message, MessageStatus.Error);
                }
            }
        }

        private async Task CompleteOrder()
        {
            try
            {
                TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
                await orderManager.CompleteOrderAsync(order);
                TooltipMessage = new TooltipMessage($"Order for client {client.Name} is successfully completed.", MessageStatus.Successful);
                OnOrderCompleted();
            }
            catch (DomainException ex)
            {
                TooltipMessage = new TooltipMessage(ex.Message, MessageStatus.Error);
            }
        }


        protected override IEnumerable<string> GetErrors(string propertyName, string propertyValue)
        {
            Validator validator = new Validator();
            IEnumerable<string> errors = new string[0];
            if (propertyName == nameof(EnteredPrice))
            {
                errors = validator.ValidatePrice(enteredPrice);
            }
            return errors;
        }

        public Order Order => order;
    }
}