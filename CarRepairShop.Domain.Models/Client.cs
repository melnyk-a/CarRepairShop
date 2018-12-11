namespace CarRepairShop.Domain.Models
{
    public sealed class Client
    {
        private readonly Person person;
        private readonly string phone;

        public Client(Person person, string phone)
        {
            this.person = person;
            this.phone = phone;
        }

        public Person Person => person;

        public string Phone => phone;
    }
}