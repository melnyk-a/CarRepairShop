namespace CarRepairShop.Wpf.Commands
{
    public abstract class AsyncCommand : Command
    {
        public override void Execute()
        {
            ExecuteAsync();
        }

        protected abstract void ExecuteAsync();
    }
}