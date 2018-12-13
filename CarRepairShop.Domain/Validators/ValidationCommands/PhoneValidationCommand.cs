using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class PhoneValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public PhoneValidationCommand(IValidator validator, string propertyName) :
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidatePhone(value);
        }
    }
}