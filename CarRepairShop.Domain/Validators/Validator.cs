using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CarRepairShop.Domain. Validators
{
    public sealed class Validator : IValidator
    {
        private const string CantBeEmpty = "can't be empty.";
        private const string Description = "Description";
        private const string IncorrectLength = "has incorrect length.";
        private const string Model = "Model";
        private const string Name = "Name";
        private const string Number = "Number";
        private const string NotLessOrEqualZero = "can't be less or equal then zero.";
        private const string NotLessZero = "can't be less then zero.";
        private const string NotMoreThen = "can't be more then";
        private const string OnlyDigits = "can contain only digits.";
        private const string Phone = "Phone";
        private const int PhoneLength = 9;
        private const string Price = "Price";
        private readonly ICollection<string> result = new List<string>();
        private const string Surname = "Surname";
        private const string Year = "Year";

        public IEnumerable<string> ValidateDescription(string description)
        {
            result.Clear();

            if (string.IsNullOrEmpty(description))
            {
                result.Add($"{Description} {CantBeEmpty}");
            }

            return result;
        }

        private bool IsDigitsOnly(string @string)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(@string);
        }

        public IEnumerable<string> ValidateModel(string model)
        {
            result.Clear();

            if (string.IsNullOrEmpty(model))
            {
                result.Add($"{Model} {CantBeEmpty}");
            }

            return result;
        }

        public IEnumerable<string> ValidateName(string name)
        {
            result.Clear();

            if (string.IsNullOrEmpty(name))
            {
                result.Add($"{Name} {CantBeEmpty}");
            }

            return result;
        }

        public IEnumerable<string> ValidateNumber(string number)
        {
            result.Clear();

            if (string.IsNullOrEmpty(number))
            {
                result.Add($"{Number} {CantBeEmpty}");
            }

            return result;
        }

        public IEnumerable<string> ValidateSurname(string surname)
        {
            result.Clear();

            if (string.IsNullOrEmpty(surname))
            {
                result.Add($"{Surname} {CantBeEmpty}");
            }

            return result;
        }

        public IEnumerable<string> ValidatePhone(string phone)
        {
            result.Clear();

            if (string.IsNullOrEmpty(phone))
            {
                result.Add($"{Phone} {CantBeEmpty}");
            }
            if (phone != null && phone.Length != PhoneLength)
            {
                result.Add($"{Phone} { IncorrectLength}");
            }
            if (phone != null && !IsDigitsOnly(phone))
            {
                result.Add($"{Phone} {OnlyDigits}");
            }

            return result;
        }

        public IEnumerable<string> ValidatePrice(string price)
        {
            result.Clear();

            if (string.IsNullOrEmpty(price))
            {
                result.Add($"{Price} {CantBeEmpty}");
            }
            bool isParsed = double.TryParse(price, out double doublePrice);
            if (!isParsed)
            {
                result.Add($"{Price} {OnlyDigits}");
            }
            if (isParsed && doublePrice < 0)
            {

                result.Add($"{Price} {NotLessZero}");
            }

            return result;
        }

        public IEnumerable<string> ValidateYear(string year)
        {
            result.Clear();

            if (string.IsNullOrEmpty(year))
            {
                result.Add($"{Year} {CantBeEmpty}");
            }
            if (year != null && !IsDigitsOnly(year))
            {
                result.Add($"{Year} {OnlyDigits}");
            }
            bool isParsed = int.TryParse(year, out int integer);
            if (isParsed & integer <= 0)
            {
                result.Add($"{Year} {NotLessOrEqualZero}");
            }
            int currentYear = (DateTime.Now).Year;
            if (isParsed & integer > currentYear)
            {
                result.Add($"{Year} {NotMoreThen} {currentYear}");
            }

            return result;
        }
    }
}