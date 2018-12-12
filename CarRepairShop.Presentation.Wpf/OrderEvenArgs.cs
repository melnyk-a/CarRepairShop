using CarRepairShop.Domain.Models;
using System;

namespace CarRepairShop.Presentation.Wpf
{
    internal sealed class OrderEvenArgs : EventArgs
    {
        private readonly Order order;

        public OrderEvenArgs(Order order)
        {
            this.order = order;
        }

        public Order Order => order;
    }
}