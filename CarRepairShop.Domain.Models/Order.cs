using System;

namespace CarRepairShop.Domain.Models
{
    public sealed class Order
    {
        private readonly Car car;
        private readonly Client client;
        private readonly string description;
        private readonly int id;

        private DateTime finishDate;
        private Person mechanic;
        private double price;
        private DateTime startDate;

        public Order(Client client, Car car, string description) :
            this(-1, client, car, description)
        {
        }

        public Order(int id, Client client, Car car, string description)
        {
            if (client.Equals(0) || Car.Equals(0) || string.IsNullOrEmpty(description))
            {
                throw new ArgumentException();
            }

            this.id = id;
            this.car = car;
            this.client = client;
            this.description = description;
        }

        public Car Car => car;

        public Client Client => client;

        public string Description => description;

        public int Id => id;

        public DateTime FinishDate
        {
            get => finishDate;
            set => finishDate = value;
        }

        public Person Mechanic
        {
            get => mechanic;
            set => mechanic = value;
        }

        public double Price
        {
            get => price;
            set => price = value;
        }

        public DateTime StartDate
        {
            get => startDate;
            set => startDate = value;
        }
    }
}