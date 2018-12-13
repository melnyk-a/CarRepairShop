using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRepairShop.Domain.Test
{
    [TestFixture]
    internal sealed class ValidatorTest
    {
        [Test]
        public void ValidateName_IsNotNull()
        {
            Validator validator = new Validator();
            object name = null;

            var errors = validator.ValidateName((string)name);

            Assert.That(errors, Has.Some.EqualTo("Name can't be empty."));
        }

        [Test]
        public void ValidateName_IsNotEmpty()
        {
            Validator validator = new Validator();
            string name = string.Empty;

            var errors = validator.ValidateName(name);

            Assert.That(errors, Has.Some.EqualTo("Name can't be empty."));
        }

        [Test]
        public void ValidateSurname_IsNotNull()
        {
            Validator validator = new Validator();
            object surname = null;

            var errors = validator.ValidateSurname((string)surname);

            Assert.That(errors, Has.Some.EqualTo("Surname can't be empty."));
        }

        [Test]
        public void ValidateSurname_IsNotEmpty()
        {
            Validator validator = new Validator();
            string surname = string.Empty;

            var errors = validator.ValidateSurname(surname);

            Assert.That(errors, Has.Some.EqualTo("Surname can't be empty."));
        }

        [Test]
        public void ValidateModel_IsNotNull()
        {
            Validator validator = new Validator();
            object model = null;

            var errors = validator.ValidateModel((string)model);

            Assert.That(errors, Has.Some.EqualTo("Model can't be empty."));
        }

        [Test]
        public void ValidateModel_IsNotEmpty()
        {
            Validator validator = new Validator();
            string model = string.Empty;

            var errors = validator.ValidateModel(model);

            Assert.That(errors, Has.Some.EqualTo("Model can't be empty."));
        }

        [Test]
        public void ValidateNumber_IsNotNull()
        {
            Validator validator = new Validator();
            object number = null;

            var errors = validator.ValidateNumber((string)number);

            Assert.That(errors, Has.Some.EqualTo("Number can't be empty."));
        }

        [Test]
        public void ValidateNumber_IsNotEmpty()
        {
            Validator validator = new Validator();
            string number = string.Empty;

            var errors = validator.ValidateNumber(number);

            Assert.That(errors, Has.Some.EqualTo("Number can't be empty."));
        }

        [Test]
        public void ValidateDescription_IsNotNull()
        {
            Validator validator = new Validator();
            object description = null;

            var errors = validator.ValidateDescription((string)description);

            Assert.That(errors, Has.Some.EqualTo("Description can't be empty."));
        }

        [Test]
        public void ValidateDescription_IsNotEmpty()
        {
            Validator validator = new Validator();
            string description = string.Empty;

            var errors = validator.ValidateDescription(description);

            Assert.That(errors, Has.Some.EqualTo("Description can't be empty."));
        }

        [Test]
        public void ValidatePhone_IsNotNull()
        {
            Validator validator = new Validator();
            object phone = null;

            var errors = validator.ValidatePhone((string)phone);

            Assert.That(errors, Has.Some.EqualTo("Phone can't be empty."));
        }

        [Test]
        public void ValidatePhone_IsNotEmpty()
        {
            Validator validator = new Validator();
            string phone = string.Empty;

            var errors = validator.ValidatePhone(phone);

            Assert.That(errors, Has.Some.EqualTo("Phone can't be empty."));
        }

        [Test]
        public void ValidatePhone_IsLengthEqual9()
        {
            Validator validator = new Validator();
            char[] phone = new char[9];

            var errors = validator.ValidatePhone(phone.ToString());

            Assert.That(errors, Has.Some.EqualTo("Phone has incorrect length."));
        }

        [Test]
        [TestCase("   ")]
        [TestCase("1abc")]
        [TestCase("abc1")]
        [TestCase("1abc1")]
        [TestCase("abc")]
        [TestCase("1.234")]
        public void ValidatePhone_IsDigitOnly(string input)
        {
            Validator validator = new Validator();          

            var errors = validator.ValidatePhone(input);

            Assert.That(errors, Has.Some.EqualTo("Phone can contain only digits."));
        }

        [Test]
        public void ValidatePhone_Return3message()
        {
            Validator validator = new Validator();
            string phone = string.Empty;

            var errors = validator.ValidatePhone(phone);

            Assert.That(errors, Has.Count.EqualTo(3));
        }

        [Test]
        public void ValidateYear_IsNotNull()
        {
            Validator validator = new Validator();
            object year = null;

            var errors = validator.ValidateYear((string)year);

            Assert.That(errors, Has.Some.EqualTo("Year can't be empty."));
        }

        [Test]
        public void ValidateYear_IsNotEmpty()
        {
            Validator validator = new Validator();
            string year = string.Empty;

            var errors = validator.ValidateYear(year);

            Assert.That(errors, Has.Some.EqualTo("Year can't be empty."));
        }

        [Test]
        [TestCase("   ")]
        [TestCase("1abc")]
        [TestCase("abc1")]
        [TestCase("1abc1")]
        [TestCase("abc")]
        [TestCase("1.234")]
        public void ValidateYear_IsDigitOnly(string input)
        {
            Validator validator = new Validator();

            var errors = validator.ValidateYear(input);

            Assert.That(errors, Has.Some.EqualTo("Year can contain only digits."));
        }

        [Test]
        [TestCase("0")]
        [TestCase("-5")]
        public void ValidateYear_IsNotLessOrEqual(string input)
        {
            Validator validator = new Validator();

            var errors = validator.ValidateYear(input);

            Assert.That(errors, Has.Some.EqualTo("Year can't be less or equal then zero."));
        }

        [Test]
        [TestCase("0")]
        [TestCase("5")]
        public void ValidateYear_IsNotGreatherCurrentYear(string increment)
        {
            Validator validator = new Validator();
            int currentYear = DateTime.Now.Year;
            string year = (currentYear + increment).ToString();

            var errors = validator.ValidateYear(year);

            Assert.That(errors, Has.Some.EqualTo($"Year can't be more then {currentYear}"));
        }


        [Test]
        public void ValidatePrice_IsNotNull()
        {
            Validator validator = new Validator();
            object price = null;

            var errors = validator.ValidatePrice((string)price);

            Assert.That(errors, Has.Some.EqualTo("Price can't be empty."));
        }

        [Test]
        public void ValidatePrice_IsNotEmpty()
        {
            Validator validator = new Validator();
            string price = string.Empty;

            var errors = validator.ValidatePrice(price);

            Assert.That(errors, Has.Some.EqualTo("Price can't be empty."));
        }

        [Test]
        [TestCase("   ")]
        [TestCase("1abc")]
        [TestCase("abc1")]
        [TestCase("1abc1")]
        [TestCase("abc")]
        public void ValidatePrice_IsDigitOnly(string input)
        {
            Validator validator = new Validator();

            var errors = validator.ValidatePrice(input);

            Assert.That(errors, Has.Some.EqualTo("Price can contain only digits."));
        }

        [Test]
        [TestCase("-5")]
        public void ValidatePrice_IsNotLess0(string input)
        {
            Validator validator = new Validator();

            var errors = validator.ValidatePrice(input);

            Assert.That(errors, Has.Some.EqualTo("Price can't be less then zero."));
        }
    }
}