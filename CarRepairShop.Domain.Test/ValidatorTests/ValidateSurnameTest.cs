using CarRepairShop.Domain.Validators;
using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidateSurnameTest
    {
        [Test]
        public void ValidateSurname_IsNotEmpty()
        {
            Validator validator = new Validator();
            string surname = string.Empty;

            var errors = validator.ValidateSurname(surname);

            Assert.That(errors, Has.Some.EqualTo("Surname can't be empty."));
        }

        [Test]
        public void ValidateSurname_IsNotNull()
        {
            Validator validator = new Validator();
            object surname = null;

            var errors = validator.ValidateSurname((string)surname);

            Assert.That(errors, Has.Some.EqualTo("Surname can't be empty."));
        }
    }
}