using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class NameValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public NameValidationCommand(IValidator validator, string propertyName):
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidateName(value);
        }
    }
}