using System;
using System.Threading.Tasks;

namespace CarRepairShop.Wpf.Commands
{
    public sealed class AsyncDelegateCommand : AsyncCommand
    {
        private readonly Func<bool> canExecuteMethod;
        private readonly Func<Task> executeMethod;

        public AsyncDelegateCommand(Func<Task> executeMethod) :
            this(executeMethod, () => true)
        {
        }

        public AsyncDelegateCommand(Func<Task> executeMethod, Func<bool> canExecuteMethod)
        {
            this.canExecuteMethod = canExecuteMethod;
            this.executeMethod = executeMethod;
        }

        private bool IsExecuting { get; set; }

        public override bool CanExecute()
        {
            return !IsExecuting && canExecuteMethod();
        }

        protected override async void ExecuteAsync()
        {
            try
            {
                IsExecuting = true;
                RaiseCanExecuteChanged();

                await executeMethod();
            }
            finally
            {
                IsExecuting = false;
                RaiseCanExecuteChanged();
            }
        }
    }
}