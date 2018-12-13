using CarRepairShop.Domain.Validators;
using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidatePhoneTest
    {
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
        public void ValidatePhone_IsLengthEqual9()
        {
            Validator validator = new Validator();
            char[] phone = new char[9];

            var errors = validator.ValidatePhone(phone.ToString());

            Assert.That(errors, Has.Some.EqualTo("Phone has incorrect length."));
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
        public void ValidatePhone_IsNotNull()
        {
            Validator validator = new Validator();
            object phone = null;

            var errors = validator.ValidatePhone((string)phone);

            Assert.That(errors, Has.Some.EqualTo("Phone can't be empty."));
        }

        [Test]
        public void ValidatePhone_Return3message()
        {
            Validator validator = new Validator();
            string phone = string.Empty;

            var errors = validator.ValidatePhone(phone);

            Assert.That(errors, Has.Count.EqualTo(3));
        }
    }
}