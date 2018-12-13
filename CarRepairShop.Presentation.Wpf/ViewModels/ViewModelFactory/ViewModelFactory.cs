using CarRepairShop.Domain;
using CarRepairShop.Domain.Models;
using CarRepairShop.Wpf.ViewModels;
using System.Collections.Generic;

namespace CarRepairShop.Presentation.Wpf.ViewModels.ViewModelFactory
{
    internal sealed class ViewModelFactory : IViewModelFactory
    {
        private readonly IOrderManager orderManager;

        public ViewModelFactory(IOrderManager orderManager)
        {
            this.orderManager = orderManager;
        }

        public ViewModel CreateAddOrderViewModel()
        {
            return new AddOrderViewModel(orderManager);
        }

        public ViewModel CreateAssignMechanicViewModel()
        {
            return new AssignMechanicViewModel(orderManager, this);
        }

        public ViewModel CreateCompleteOrderViewModel()
        {
            return new CompleteOrderViewModel(orderManager, this);
        }

        public ViewModel CreateDetailOrderViewModel(Order order)
        {
            return new DetailOrderViewModel(order, this);
        }

        public ViewModel CreateFreeOrderViewModel(Order order, IEnumerable<PersonViewModel> mechanics)
        {
            return new FreeOrderViewModel(order, mechanics, orderManager, this);
        }

        public ViewModel CreateMainWindowViewModel()
        {
            return new MainWindowViewModel(this);
        }

        public ViewModel CreatePersonViewModel(Person person)
        {
            return new PersonViewModel(person);
        }

        public ViewModel CreateReportViewModel()
        {
            return new ReportViewModel(orderManager, this);
        }

        public ViewModel CreateUncompleteOrderViewModel(Order order)
        {
            return new UncompleteOrderViewModel(order, orderManager, this);
        }
    }
}