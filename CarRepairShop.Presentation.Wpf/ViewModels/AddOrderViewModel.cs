using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Presentation.Wpf.ToolTips;
using CarRepairShop.Wpf.Attributes;
using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class AddOrderViewModel : ValidatableViewModel, ITooltipMessageViewModel
    {
        private readonly ICommand addOrderCommand;
        private readonly IOrderManager orderManager;

        private string description = string.Empty;
        private string model = string.Empty;
        private string name = string.Empty;
        private string number = string.Empty;
        private string phone = string.Empty;
        private string surname = string.Empty;
        private TooltipMessage tooltipMessage;
        private string year = string.Empty;      

        public AddOrderViewModel(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
            addOrderCommand = new AsyncDelegateCommand(AddOrderAsync, () => CanAddOrder);
        }

        [RaiseCanExecuteDependsUpon(nameof(CanAddOrder))]
        public ICommand AddOrderCommand => addOrderCommand;

        [DependsUponProperty(nameof(Name))]
        [DependsUponProperty(nameof(Surname))]
        [DependsUponProperty(nameof(Phone))]
        [DependsUponProperty(nameof(Model))]
        [DependsUponProperty(nameof(Year))]
        [DependsUponProperty(nameof(Number))]
        [DependsUponProperty(nameof(Description))]
        public bool CanAddOrder => !HasErrors;

        [ValidatableProperty]
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        [ValidatableProperty]
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        [ValidatableProperty]
        public string Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        [ValidatableProperty]
        public string Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        [ValidatableProperty]
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        [ValidatableProperty]
        public string Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
        }

        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }

        [ValidatableProperty]
        public string Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        private async Task AddOrderAsync()
        {
            Validate();
            if (!HasErrors)
            {
                TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);

                Client client = new Client(new Person(name, surname), phone);
                Car car = new Car(model, int.Parse(year), number);
                Order order = new Order(client, car, description);

                try
                {
                    await orderManager.AddOrderAync(order);
                    TooltipMessage = new TooltipMessage($"Order for client {name} {surname} is successfully added.", MessageStatus.Successful);
                }
                catch(DomainException ex)
                {
                    TooltipMessage = new TooltipMessage(ex.Message, MessageStatus.Error);
                }
            }
        }

        protected override IEnumerable<string> GetErrors(string propertyName, string propertyValue)
        {
            return orderManager.ValidateProperty(propertyName, propertyValue);
        }
    }
}