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
    internal sealed class AddOrderViewModel : ValidatableViewModel, ITooltipMessageViewModel
    {
        private readonly ICommand addOrderCommand;
        private readonly IOrderManager orderManager;

        private string name = string.Empty;
        private string surname = string.Empty;
        private string phone = string.Empty;
        private string model = string.Empty;
        private string number = string.Empty;
        private string description = string.Empty;
        private string year = string.Empty;
        private TooltipMessage tooltipMessage;

        public AddOrderViewModel(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
            addOrderCommand = new AsyncDelegateCommand(AddOrderAsync, () => CanAddOrder);
        }

        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }
        public string Phone
        {
            get => phone;
            set => SetProperty(ref phone, value);
        }

        public string Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }
        public string Year
        {
            get => year;
            set => SetProperty(ref year, value);
        }

        public string Number
        {
            get => number;
            set => SetProperty(ref number, value);
        }

        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        public string Surname
        {
            get => surname;
            set => SetProperty(ref surname, value);
        }

        [DependsUponProperty(nameof(Name))]
        [DependsUponProperty(nameof(Surname))]
        [DependsUponProperty(nameof(Phone))]
        [DependsUponProperty(nameof(Model))]
        [DependsUponProperty(nameof(Year))]
        [DependsUponProperty(nameof(Number))]
        [DependsUponProperty(nameof(Description))]
        public bool CanAddOrder => !HasErrors;

        [RaiseCanExecuteDependsUpon(nameof(CanAddOrder))]
        public ICommand AddOrderCommand => addOrderCommand;


        public TooltipMessage TooltipMessage
        {
            get => tooltipMessage;
            set => SetProperty(ref tooltipMessage, value);
        }

        private async Task AddOrderAsync()
        {
            Validate();
            if (!HasErrors)
            {
                TooltipMessage = new TooltipMessage("Pending...", MessageStatus.Pending);

                Client client = new Client(new Person(name, surname), phone);
                Car car = new Car(model, int.Parse(year), number);
                Order order = new Order(client, car, description, DateTime.Now);

                await orderManager.AddOrder(order);

                TooltipMessage = new TooltipMessage($"Order for client {name} {surname} successfully added.", MessageStatus.Successful);
            }
        }

        protected override IEnumerable<string> GetErrors(string propertyName)
        {
            Validator validator = new Validator();
            IEnumerable<string> errors;
            switch (propertyName)
            {
                case nameof(Name):
                    errors = validator.ValidateName(Name);
                    break;
                case nameof(Surname):
                    errors = validator.ValidateSurname(Surname);
                    break;
                case nameof(Phone):
                    errors = validator.ValidatePhone(Phone);
                    break;
                case nameof(Model):
                    errors = validator.ValidateModel(Model);
                    break;
                case nameof(Number):
                    errors = validator.ValidateNumber(Number);
                    break;
                case nameof(Year):
                    errors = validator.ValidateYear(Year);
                    break;
                case nameof(Description):
                    errors = validator.ValidateDescription(Description);
                    break;
                default:
                    errors = new string[0];
                    break;
            }

            return errors;
        }
    }
}