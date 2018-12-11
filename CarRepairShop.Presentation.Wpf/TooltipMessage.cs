namespace CarRepairShop.Presentation.Wpf
{
    internal sealed class TooltipMessage
    {
        private readonly string message;
        private readonly MessageStatus status;
        
        public TooltipMessage(string message, MessageStatus status)
        {
            this.message = message;
            this.status = status;
        }

        public string Message => message;

        public MessageStatus Status => status;
    }
}