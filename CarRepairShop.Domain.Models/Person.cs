using System;

namespace CarRepairShop.Domain.Models
{
    public sealed class Person
    {
        private readonly int id;
        private readonly string name;
        private readonly string surname;

        public Person(string name, string surname) :
            this(-1, name, surname)
        {
        }

        public Person(int id, string name, string surname)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(surname))
            {
                throw new ArgumentException();
            }

            this.id = id;
            this.name = name;
            this.surname = surname;
        }

        public int Id => id;

        public string Name => name;

        public string Surname => surname;
    }
}