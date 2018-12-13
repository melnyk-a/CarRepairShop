using System;

namespace CarRepairShop.Presentation.Wpf.ToolTips
{
    internal sealed class TooltipMessageEventArgs : EventArgs
    {
        private readonly TooltipMessage message;

        public TooltipMessageEventArgs(TooltipMessage message)
        {
            this.message = message;
        }

        public TooltipMessage Message => message;
    }
}