using CarRepairShop.Domain.Models;
using CarRepairShop.Presentation.Wpf.ViewModels.ViewModelFactory;
using CarRepairShop.Wpf.ViewModels;
using System;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class DetailOrderViewModel : ViewModel
    {
        private readonly PersonViewModel client;
        private readonly IViewModelFactory factory;
        private readonly string finishDate;
        private readonly PersonViewModel mechanic;
        private readonly Order order;
        private readonly string price;

        public DetailOrderViewModel(Order order, IViewModelFactory factory)
        {
            this.factory = factory;
            this.order = order;
            client = (PersonViewModel)factory.CreatePersonViewModel(order.Client.Person);
            mechanic = order.Mechanic != null ? new PersonViewModel(order.Mechanic) : null;
            finishDate = order.FinishDate != default(DateTime) ? order.FinishDate.ToShortDateString() : string.Empty;
            price = order.Price != 0 ? string.Format("{0:0.00}", order.Price) : string.Empty;
        }

        public string CarModel => order.Car.Model;

        public string FinishDate => finishDate;

        public PersonViewModel Client => client;

        public PersonViewModel Mechanic => mechanic;

        public string Price => price;

        public string StartDate => order.StartDate.ToShortDateString();
    }
}