using System;

namespace CarRepairShop.Domain.Models
{
    public sealed class Client
    {
        private readonly Person person;
        private readonly string phone;

        public Client(Person person, string phone)
        {
            if (person.Equals(0) || string.IsNullOrEmpty(phone))
            {
                throw new ArgumentException();
            }

            this.person = person;
            this.phone = phone;
        }

        public Person Person => person;

        public string Phone => phone;
    }
}