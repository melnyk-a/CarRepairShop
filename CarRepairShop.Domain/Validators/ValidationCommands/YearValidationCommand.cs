using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class YearValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public YearValidationCommand(IValidator validator, string propertyName) :
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidateYear(value);
        }
    }
}