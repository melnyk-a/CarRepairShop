using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class PriceValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public PriceValidationCommand(IValidator validator, string propertyName) :
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidatePrice(value);
        }
    }
}