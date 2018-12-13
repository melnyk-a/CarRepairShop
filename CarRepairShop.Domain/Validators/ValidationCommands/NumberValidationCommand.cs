using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class NumberValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public NumberValidationCommand(IValidator validator, string propertyName) :
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidateNumber(value);
        }
    }
}