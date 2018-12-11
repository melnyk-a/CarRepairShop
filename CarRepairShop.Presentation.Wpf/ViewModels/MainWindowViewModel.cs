using CarRepairShop.Wpf.Commands;
using CarRepairShop.Wpf.ViewModels;
using System.Windows.Input;

namespace CarRepairShop.Presentation.Wpf.ViewModels
{
    internal sealed class MainWindowViewModel : ViewModel
    {
        private readonly ITooltipMessageViewModel addOrder;
        private readonly ICommand addOrderCommand;
        
        private ITooltipMessageViewModel current;

        public MainWindowViewModel(AddOrderViewModel addOrderViewModel)
        {
            addOrder = addOrderViewModel;
            addOrderCommand = new DelegateCommand(() => Current = addOrder);
        }

        public ITooltipMessageViewModel Current
        {
            get => current;
            private set => SetProperty(ref current, value);
        }

        public ICommand AddOrderCommand => addOrderCommand;
    }
}