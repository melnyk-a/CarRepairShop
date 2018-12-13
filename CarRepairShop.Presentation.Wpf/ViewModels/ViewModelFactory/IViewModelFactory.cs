using CarRepairShop.Domain.Models;
using CarRepairShop.Wpf.ViewModels;
using System.Collections.Generic;

namespace CarRepairShop.Presentation.Wpf.ViewModels.ViewModelFactory
{
    internal interface IViewModelFactory
    {
        ViewModel CreateAddOrderViewModel();
        ViewModel CreateAssignMechanicViewModel();
        ViewModel CreateCompleteOrderViewModel();
        ViewModel CreateDetailOrderViewModel(Order order);
        ViewModel CreateFreeOrderViewModel(Order order, IEnumerable<PersonViewModel> mechanics);
        ViewModel CreateMainWindowViewModel();
        ViewModel CreatePersonViewModel(Person person);
        ViewModel CreateReportViewModel();
        ViewModel CreateUncompleteOrderViewModel(Order order);
    }
}