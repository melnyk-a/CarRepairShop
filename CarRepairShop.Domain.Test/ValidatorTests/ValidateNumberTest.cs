using CarRepairShop.Domain.Validators;
using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidateNumberTest
    {
        [Test]
        public void ValidateNumber_IsNotEmpty()
        {
            Validator validator = new Validator();
            string number = string.Empty;

            var errors = validator.ValidateNumber(number);

            Assert.That(errors, Has.Some.EqualTo("Number can't be empty."));
        }

        [Test]
        public void ValidateNumber_IsNotNull()
        {
            Validator validator = new Validator();
            object number = null;

            var errors = validator.ValidateNumber((string)number);

            Assert.That(errors, Has.Some.EqualTo("Number can't be empty."));
        }
    }
}