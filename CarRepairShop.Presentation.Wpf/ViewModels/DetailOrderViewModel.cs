using CarRepairShop.Domain.Models;
using System;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class DetailOrderViewModel
    {
        private readonly PersonViewModel client;
        private readonly string finishDate;
        private readonly PersonViewModel mechanic;
        private readonly Order order;
        private readonly string price;

        public DetailOrderViewModel(Order order)
        {
            this.order = order;
            client = new PersonViewModel(order.Client.Person);
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