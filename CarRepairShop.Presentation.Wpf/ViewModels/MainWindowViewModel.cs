using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly ITooltipMessageViewModel addOrder;
        private readonly ITooltipMessageViewModel report;
        private readonly ICommand addOrderCommand;
        private readonly ICommand reportCommand;

        private ITooltipMessageViewModel current;

        public MainWindowViewModel(AddOrderViewModel addOrderViewModel, ReportViewModel reportViewModel)
        {
            addOrder = addOrderViewModel;
            report = reportViewModel;
            addOrderCommand = new DelegateCommand(() => Current = addOrder);
            reportCommand = new DelegateCommand(() => Current = report);
        }

        public ITooltipMessageViewModel Current
        {
            get => current;
            private set => SetProperty(ref current, value);
        }

        public ICommand AddOrderCommand => addOrderCommand;

        public ICommand ReportCommand => reportCommand;
    }
}