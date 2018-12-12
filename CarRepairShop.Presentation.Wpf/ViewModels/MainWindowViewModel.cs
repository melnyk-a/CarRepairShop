using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly ViewModel addOrder;
        private readonly ViewModel report;
        private readonly ICommand addOrderCommand;
        private readonly ICommand reportCommand;

        private object current;

        public MainWindowViewModel(AddOrderViewModel addOrderViewModel, ReportViewModel reportViewModel)
        {
            addOrder = addOrderViewModel;
            report = reportViewModel;
            addOrderCommand = new DelegateCommand(() => Current = addOrder);
            reportCommand = new DelegateCommand(() => Current = report);
        }

        public object Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        public ICommand AddOrderCommand => addOrderCommand;

        public ICommand ReportCommand => reportCommand;
    }
}