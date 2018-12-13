using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidatePriceTest
    {

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
        public void ValidatePrice_IsNotEmpty()
        {
            Validator validator = new Validator();
            string price = string.Empty;

            var errors = validator.ValidatePrice(price);

            Assert.That(errors, Has.Some.EqualTo("Price can't be empty."));
        }


        [Test]
        [TestCase("-5")]
        public void ValidatePrice_IsNotLess0(string input)
        {
            Validator validator = new Validator();

            var errors = validator.ValidatePrice(input);

            Assert.That(errors, Has.Some.EqualTo("Price can't be less then zero."));
        }

        [Test]
        public void ValidatePrice_IsNotNull()
        {
            Validator validator = new Validator();
            object price = null;

            var errors = validator.ValidatePrice((string)price);

            Assert.That(errors, Has.Some.EqualTo("Price can't be empty."));
        }
    }
}