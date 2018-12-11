using CarRepairShop.Presentation.Wpf.Views;
using Ninject;
using Ninject.Extensions.Conventions;
using System.Windows;

namespace CarRepairShop.Presentation.Wpf
{
    public partial class App : Application
    {
        private StandardKernel CreateContainer()
        {
            var container = new StandardKernel();

            container.Bind(
                configurator => configurator
                    .From("CarRepairShop.Data.SqlClient", "CarRepairShop.Domain")
                    .SelectAllClasses()
                    .BindAllInterfaces()
            );
            return container;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var container = CreateContainer();
            MainWindowView mainWindow = container.Get<MainWindowView>();
            mainWindow.Show();
        }
    }
}