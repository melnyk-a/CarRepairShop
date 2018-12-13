using System.Collections.Generic;
using System.Runtime.CompilerServices;
using CarRepairShop.Domain.Validators;

namespace CarRepairShop.Domain.Test.OrderManagerTests
{
    internal sealed class ValidatorMock : IValidator
    {
        private readonly ICollection<string> calls = new List<string>();

        public ICollection<string> Calls => calls;

        private IEnumerable<string> AddCall([CallerMemberName] string methodName = null)
        {
            calls.Add(methodName);
            return new string[0];
        }

        public IEnumerable<string> ValidateDescription(string description)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidateModel(string model)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidateName(string name)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidateNumber(string number)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidatePhone(string phone)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidatePrice(string price)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidateSurname(string surname)
        {
            return AddCall();
        }

        public IEnumerable<string> ValidateYear(string year)
        {
            return AddCall();
        }
    }
}