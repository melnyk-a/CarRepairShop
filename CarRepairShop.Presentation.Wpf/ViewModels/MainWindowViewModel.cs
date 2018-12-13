using CarRepairShop.Presentation.Wpf.ViewModels.ViewModelFactory;
using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly ICommand addOrderCommand;
        private readonly ICommand reportCommand;
        private readonly ICommand assignMechanicCommand;
        private readonly ICommand completeOrderCommand;

        private object current;

        public MainWindowViewModel(IViewModelFactory factory)
        {
            addOrderCommand = new DelegateCommand(() => Current = factory.CreateAddOrderViewModel());
            reportCommand = new DelegateCommand(() => Current = factory.CreateReportViewModel());
            assignMechanicCommand = new DelegateCommand(() => Current = factory.CreateAssignMechanicViewModel());
            completeOrderCommand = new DelegateCommand(() => Current = factory.CreateCompleteOrderViewModel());
        }

        public object Current
        {
            get => current;
            set => SetProperty(ref current, value);
        }

        public ICommand AddOrderCommand => addOrderCommand;

        public ICommand AssignMechanicCommand => assignMechanicCommand;

        public ICommand CompleteOrderCommand => completeOrderCommand;

        public ICommand ReportCommand => reportCommand;
    }
}