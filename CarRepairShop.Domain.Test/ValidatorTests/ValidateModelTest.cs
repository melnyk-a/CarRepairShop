using CarRepairShop.Domain.Validators;
using NUnit.Framework;

namespace CarRepairShop.Domain.Test.ValidatorTests
{
    [TestFixture]
    internal sealed class ValidateModelTest
    {
        [Test]
        public void ValidateModel_IsNotEmpty()
        {
            Validator validator = new Validator();
            string model = string.Empty;

            var errors = validator.ValidateModel(model);

            Assert.That(errors, Has.Some.EqualTo("Model can't be empty."));
        }

        [Test]
        public void ValidateModel_IsNotNull()
        {
            Validator validator = new Validator();
            object model = null;

            var errors = validator.ValidateModel((string)model);

            Assert.That(errors, Has.Some.EqualTo("Model can't be empty."));
        }
    }
}