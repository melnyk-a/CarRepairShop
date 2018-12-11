using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CarRepairShop.Domain
{
    public sealed class Validator
    {
        private const int PhoneLength = 9;
        private ICollection<string> result = new List<string>();

        private bool IsNullOrEmpty(string @string)
        {
            return string.IsNullOrEmpty(@string);
        }

        private bool IsDigitsOnly(string @string)
        {
            Regex regex = new Regex("^[0-9]+$");
            return regex.IsMatch(@string);
        }

        public IEnumerable<string> ValidateName(string name)
        {
            result.Clear();

            if (IsNullOrEmpty(name))
            {
                result.Add("Name can't be empty.");
            }
            return result;
        }

        public IEnumerable<string> ValidateSurname(string surname)
        {
            result.Clear();

            if (IsNullOrEmpty(surname))
            {
                result.Add("Surname can't be empty.");
            }
            return result;
        }

        public IEnumerable<string> ValidatePhone(string phone)
        {
            result.Clear();

            if (IsNullOrEmpty(phone))
            {
                result.Add("Phone can't be empty.");
            }
            if (phone.Length != PhoneLength)
            {
                result.Add("Incorrect phone length.");
            }
            if (!IsDigitsOnly(phone))
            {
                result.Add("Phone can contain only digits.");
            }
            return result;
        }

        public IEnumerable<string> ValidateModel(string model)
        {
            result.Clear();

            if (IsNullOrEmpty(model))
            {
                result.Add("Model can't be empty.");
            }
            return result;
        }

        public IEnumerable<string> ValidateNumber(string number)
        {
            result.Clear();

            if (IsNullOrEmpty(number))
            {
                result.Add("Number can't be empty.");
            }
            return result;
        }

        public IEnumerable<string> ValidateYear(string year)
        {
            result.Clear();

            if (IsNullOrEmpty(year))
            {
                result.Add("Year can't be empty.");
            }
            if (!IsDigitsOnly(year))
            {
                result.Add("Year can contain only digits.");
            }
            if (year.Length < 5 && int.TryParse(year, out int integer))
            {
                int currentYear = (DateTime.Now).Year;
                if (integer > currentYear)
                {
                    result.Add($"Year can't be more then {currentYear}");
                }
            }
            return result;
        }

        public IEnumerable<string> ValidateDescription(string description)
        {
            result.Clear();

            if (IsNullOrEmpty(description))
            {
                result.Add("Description can't be empty.");
            }
            return result;
        }
    }
}