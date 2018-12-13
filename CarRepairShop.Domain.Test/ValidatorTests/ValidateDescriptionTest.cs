using CarRepairShop.Domain.Validators;
using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidateDescriptionTest
    {
        [Test]
        public void ValidateDescription_IsNotEmpty()
        {
            Validator validator = new Validator();
            string description = string.Empty;

            var errors = validator.ValidateDescription(description);

            Assert.That(errors, Has.Some.EqualTo("Description can't be empty."));
        }

        [Test]
        public void ValidateDescription_IsNotNull()
        {
            Validator validator = new Validator();
            object description = null;

            var errors = validator.ValidateDescription((string)description);

            Assert.That(errors, Has.Some.EqualTo("Description can't be empty."));
        }
    }
}