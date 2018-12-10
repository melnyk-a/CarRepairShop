using CarRepairShop.Presentation.Wpf.ViewModels;
using System.Windows;

namespace CarRepairShop.Presentation.Wpf.Views
{
    internal partial class MainWindowView : Window
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}