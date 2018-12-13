using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands.ValidationCommandFactories
{
    public interface IValidationCommandFactory
    {
        IEnumerable<IValidationCommand> CreateValidationCommands();
    }
}