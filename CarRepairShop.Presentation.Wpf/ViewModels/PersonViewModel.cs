using CarRepairShop.Domain.Models;
using CarRepairShop.Wpf.ViewModels;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class PersonViewModel : ViewModel
    {
        private readonly Person person;

        public PersonViewModel(Person person)
        {
            this.person = person;
        }

        public string Name => $"{person.Name} {person.Surname}";

        public Person Person => person;
    }
}