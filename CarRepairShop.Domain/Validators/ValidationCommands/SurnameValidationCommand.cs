using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class SurnameValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public SurnameValidationCommand(IValidator validator, string propertyName) :
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidateSurname(value);
        }
    }
}