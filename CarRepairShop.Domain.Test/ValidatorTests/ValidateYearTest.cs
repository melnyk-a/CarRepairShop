using NUnit.Framework;
using System;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidateYearTest
    {
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
        public void ValidateYear_IsNotEmpty()
        {
            Validator validator = new Validator();
            string year = string.Empty;

            var errors = validator.ValidateYear(year);

            Assert.That(errors, Has.Some.EqualTo("Year can't be empty."));
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
        [TestCase("0")]
        [TestCase("-5")]
        public void ValidateYear_IsNotLessOrEqual(string input)
        {
            Validator validator = new Validator();

            var errors = validator.ValidateYear(input);

            Assert.That(errors, Has.Some.EqualTo("Year can't be less or equal then zero."));
        }

        [Test]
        public void ValidateYear_IsNotNull()
        {
            Validator validator = new Validator();
            object year = null;

            var errors = validator.ValidateYear((string)year);

            Assert.That(errors, Has.Some.EqualTo("Year can't be empty."));
        }
    }
}