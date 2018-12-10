namespace CarRepairShop.Domain.Models
{
    public sealed class Car
    {
        private readonly string number;
        private readonly string model;
        private readonly int year;

        public Car(string model, int year, string number)
        {
            this.number = number;
            this.model = model;
            this.year = year;
        }

        public string Number => number;

        public string Model => model;

        public int Year => year;
    }
}