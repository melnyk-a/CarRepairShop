using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal abstract class ValidationCommand : IValidationCommand
    {
        private readonly string propertyName;

        public ValidationCommand(string propertyName)
        {
            this.propertyName = propertyName;
        }

        public bool CanValidate(string propertyName)
        {
            return this.propertyName == propertyName;
        }

        public abstract IEnumerable<string> Validate(string value);

    }
}