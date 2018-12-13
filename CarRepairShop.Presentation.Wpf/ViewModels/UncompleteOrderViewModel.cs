using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
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
        private readonly PersonViewModel client;
        private readonly ICommand completeOrderCommand;
        private readonly Order order;
        private readonly ICommand setPriceCommand;
        private readonly IOrderManager orderManager;

        private string currentPrice;
        private string price;
        private TooltipMessage tooltipMessage;

        public UncompleteOrderViewModel(Order order, IOrderManager orderManager)
        {
            this.orderManager = orderManager;
            this.order = order;
            client = new PersonViewModel(order.Client.Person);
            currentPrice = order.Price.ToString();

            setPriceCommand = new AsyncDelegateCommand(SetPrice, () => CanSetPrice);
            completeOrderCommand = new AsyncDelegateCommand(CompleteOrder);
        }

        [DependsUponProperty(nameof(Price))]
        public bool CanSetPrice => !HasErrors;

        public string CarModel => order.Car.Model;

        public PersonViewModel Client => client;

        public ICommand CompleteOrderCommand => completeOrderCommand;

        public string CurrentPrice
        {
            get => currentPrice;
            set => SetProperty(ref currentPrice, value);
        }

        [ValidatableProperty]
        public string Price
        {
            get => price;
            set => SetProperty(ref price, value);
        }

        public Order Order => order;

        [RaiseCanExecuteDependsUpon(nameof(CanSetPrice))]
        public ICommand SetPriceCommand => setPriceCommand;

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set
            {
                tooltipMessage = value;
                OnMessageCreated(tooltipMessage);
            }
        }

        public event EventHandler<TooltipMessageEventArgs> MessageCreated;
        public event EventHandler<OrderEvenArgs> OrderCompleted;

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
            return orderManager.ValidateProperty(propertyName, propertyValue);
        }

        private void OnMessageCreated(TooltipMessage message)
        {
            MessageCreated?.Invoke(this, new TooltipMessageEventArgs(message));
        }

        private void OnOrderCompleted()
        {
            OrderCompleted?.Invoke(this, new OrderEvenArgs(order));
        }

        private async Task SetPrice()
        {
            Validate();
            if (!HasErrors)
            {
                TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);
                try
                {
                    await orderManager.SetPriceAsync(order, double.Parse(price));
                    CurrentPrice = Price;
                    TooltipMessage = new TooltipMessage($"Price for client {client.Name} is successfully set.", MessageStatus.Successful);
                }
                catch (DomainException ex)
                {
                    TooltipMessage = new TooltipMessage(ex.Message, MessageStatus.Error);
                }
            }
        }
    }
}