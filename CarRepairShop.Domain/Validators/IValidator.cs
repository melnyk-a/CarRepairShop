using System.Collections.Generic;

namespace CarRepairShop.Domain.Validators
{
    public interface IValidator
    {
        IEnumerable<string> ValidateDescription(string description);
        IEnumerable<string> ValidateModel(string model);
        IEnumerable<string> ValidateName(string name);
        IEnumerable<string> ValidateNumber(string number);
        IEnumerable<string> ValidateSurname(string surname);
        IEnumerable<string> ValidatePhone(string phone);
        IEnumerable<string> ValidatePrice(string price);
        IEnumerable<string> ValidateYear(string year);
    }
}