using CarRepairShop.Domain.Models;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class PersonViewModel
    {
        private readonly Person person;

        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public string Name => $"{person.Name} {person.Surname}";
    }
}