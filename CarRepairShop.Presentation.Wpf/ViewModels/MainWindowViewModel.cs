using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly ViewModel addOrder;
        private readonly ViewModel assignMechanic;
        private readonly ViewModel report;
        private readonly ICommand addOrderCommand;
        private readonly ICommand reportCommand;
        private readonly ICommand assignMechanicCommand;


        private object current;

        public MainWindowViewModel(AddOrderViewModel addOrderViewModel, ReportViewModel reportViewModel, AssignMechanicViewModel assignMechanicViewModel)
        {
            addOrder = addOrderViewModel;
            report = reportViewModel;
            assignMechanic = assignMechanicViewModel;

            addOrderCommand = new DelegateCommand(() => Current = addOrder);
            reportCommand = new DelegateCommand(() => Current = report);
            assignMechanicCommand = new DelegateCommand(() => Current = assignMechanic);
        }

        public object Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        public ICommand AddOrderCommand => addOrderCommand;

        public ICommand ReportCommand => reportCommand;
        public ICommand AssignMechanicCommand => assignMechanicCommand;
    }
}