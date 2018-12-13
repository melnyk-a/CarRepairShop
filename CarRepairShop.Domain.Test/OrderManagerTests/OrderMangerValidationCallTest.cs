using CarRepairShop.Domain.Validators;
using CarRepairShop.Domain.Validators.ValidationCommands.ValidationCommandFactories;
using NUnit.Framework;

namespace CarRepairShop.Domain.Test.OrderManagerTests
{
    [TestFixture]
    internal sealed class OrderMangerValidationCallTest
    {
        [Test]
        public void ValidateProperty_DescriptionCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Description", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidateDescription)));
        }

        [Test]
        public void ValidateProperty_NameCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Name", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidateName)));
        }

        [Test]
        public void ValidateProperty_NumberCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Number", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidateNumber)));
        }

        [Test]
        public void ValidateProperty_ModelCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Model", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidateModel)));
        }

        [Test]
        public void ValidateProperty_PhoneCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Phone", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidatePhone)));
        }
        [Test]
        public void ValidateProperty_PriceCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Price", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidatePrice)));
        }

        [Test]
        public void ValidateProperty_SurnameCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Surname", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidateSurname)));
        }

        [Test]
        public void ValidateProperty_YearCall()
        {
            var vallidator = new ValidatorMock();
            IOrderManager orderManager = new OrderManager(null, new ValidationCommandFactory(vallidator));

            orderManager.ValidateProperty("Year", string.Empty);

            Assert.That(vallidator.Calls, Has.Exactly(1).EqualTo(nameof(Validator.ValidateYear)));
        }
    }
}