using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands.ValidationCommandFactories
{
    public sealed class ValidationCommandFactory : IValidationCommandFactory
    {
        private readonly IValidator validator;

        public ValidationCommandFactory(IValidator validator)
        {
            this.validator = validator;
        }

        public IEnumerable<IValidationCommand> CreateValidationCommands()
        {
            return new List<IValidationCommand>()
            {
                new NameValidationCommand(validator, "Name"),
                new SurnameValidationCommand(validator, "Surname"),
                new PhoneValidationCommand(validator, "Phone"),
                new ModelValidationCommand(validator, "Model"),
                new NumberValidationCommand(validator, "Number"),
                new YearValidationCommand(validator, "Year"),
                new DescriptionValidationCommand(validator, "Description"),
                new PriceValidationCommand(validator, "Price")
            };
        }
    }
}