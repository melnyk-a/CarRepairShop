using CarRepairShop.Domain.Models;
using System;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class DetailOrderViewModel
    {
        private readonly Order order;

        public DetailOrderViewModel(Order order)
        {
            this.order = order;
        }

        public string Client => $"{order.Client.Person.Name} {order.Client.Person.Surname}";

        public string CarModel => order.Car.Model;

        public string StartDate => order.StartDate.ToShortDateString();

        public string FinishDate => order.FinishDate.ToShortDateString();

        public string Mechanic => order.Mechanic != null ? $"{order.Mechanic.Name} {order.Mechanic.Surname}" : string.Empty;

        public string Price => order.Price!=0?string.Format("{0:0.00}", order.Price) : string.Empty;

    }
}