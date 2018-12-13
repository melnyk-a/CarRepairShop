using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    internal sealed class DescriptionValidationCommand : ValidationCommand
    {
        private readonly IValidator validator;

        public DescriptionValidationCommand(IValidator validator, string propertyName) :
            base(propertyName)
        {
            this.validator = validator;
        }

        public override IEnumerable<string> Validate(string value)
        {
            return validator.ValidateDescription(value);
        }
    }
}