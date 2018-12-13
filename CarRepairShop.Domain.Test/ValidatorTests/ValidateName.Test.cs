using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidateNameTest
    {
        [Test]
        public void ValidateName_IsNotEmpty()
        {
            Validator validator = new Validator();
            string name = string.Empty;

            var errors = validator.ValidateName(name);

            Assert.That(errors, Has.Some.EqualTo("Name can't be empty."));
        }

        [Test]
        public void ValidateName_IsNotNull()
        {
            Validator validator = new Validator();
            object name = null;

            var errors = validator.ValidateName((string)name);

            Assert.That(errors, Has.Some.EqualTo("Name can't be empty."));
        }
    }
}