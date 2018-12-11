using System;

namespace CarRepairShop.Domain.Models
{
    public sealed class Order
    {
        private readonly Car car;
        private readonly Client client;
        private readonly string description;
        private readonly int id;
        private readonly DateTime startDate;

        private DateTime finishDate;
        private Person mechanic;
        private double price;

        public Order(int id, Client client, Car car, string description, DateTime startDate)
        {
            this.id = id;
            this.car = car;
            this.client = client;
            this.description = description;
            this.startDate = startDate;
        }

        public Order(Client client, Car car, string description, DateTime startDate) :
            this(-1, client, car, description, startDate)
        {
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

        public DateTime StartDate => startDate;
    }
}