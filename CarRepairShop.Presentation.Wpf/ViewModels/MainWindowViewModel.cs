using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly AddOrderViewModel addOrder;
        private readonly AssignMechanicViewModel assignMechanic;
        private readonly ReportViewModel report;
        private readonly CompleteOrderViewModel completeOrder;
        private readonly ICommand addOrderCommand;
        private readonly ICommand reportCommand;
        private readonly ICommand assignMechanicCommand;
        private readonly ICommand completeOrderCommand;


        private object current;

        public MainWindowViewModel(AddOrderViewModel addOrderViewModel, ReportViewModel reportViewModel, AssignMechanicViewModel assignMechanicViewModel, CompleteOrderViewModel completeOrderViewModel)
        {
            addOrder = addOrderViewModel;
            report = reportViewModel;
            assignMechanic = assignMechanicViewModel;
            completeOrder = completeOrderViewModel;

            addOrderCommand = new DelegateCommand(() => Current = addOrder);

            reportCommand = new DelegateCommand(() =>
            {
               
                Current = report;
                report.LoadOrders();
            });

            assignMechanicCommand = new DelegateCommand(() =>
            {
               
                Current = assignMechanic;
                assignMechanic.LoadOrders();
            });
            completeOrderCommand = new DelegateCommand(() =>
            {
               
                Current = completeOrder;
                completeOrder.LoadOrders();
            });
        }

        public object Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        public ICommand AddOrderCommand => addOrderCommand;

        public ICommand ReportCommand => reportCommand;
        public ICommand CompleteOrderCommand => completeOrderCommand;
        public ICommand AssignMechanicCommand => assignMechanicCommand;
    }
}