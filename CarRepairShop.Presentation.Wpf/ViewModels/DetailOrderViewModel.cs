using CarRepairShop.Domain.Models;
using System;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class DetailOrderViewModel
    {
        private readonly PersonViewModel client;
        private readonly PersonViewModel mechanic;
        private readonly Order order;

        public DetailOrderViewModel(Order order)
        {
            this.order = order;
            client = new PersonViewModel(order.Client.Person);
            mechanic = order.Mechanic != null ? new PersonViewModel(order.Mechanic) : null;
        }

        public PersonViewModel Client => client;

        public string CarModel => order.Car.Model;

        public string StartDate => order.StartDate.ToShortDateString();

        public string FinishDate => order.FinishDate.ToShortDateString();

        public PersonViewModel Mechanic => mechanic;

        public string Price => order.Price!=0?string.Format("{0:0.00}", order.Price) : string.Empty;

    }
}