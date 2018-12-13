using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators.ValidationCommands
{
    public interface IValidationCommand
    {
        bool CanValidate(string propertyName);
        IEnumerable<string> Validate(string propertyName);
    }
}