using System;
using Xunit;
using Moq;
using FluentAssertions;
using BusinessLogic.Exceptions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCaseTests
    {
        [Fact]
        public void Given_No_CustomerRegistration_When_Call_Register_Then_Throw_MissingCustomerRegistration_Exception()
        {
            var useCase = new RegisterCustomerUseCase(null);
            Action register = () => useCase.Register(null);
            register.Should().ThrowExactly<MissingCustomerRegistration>()
                             .WithMessage("Missing customer registration.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("  ")]
        [InlineData("  \t\n ")]
        public void Given_No_FirstName_When_Call_Register_Then_Throw_MissingFirstName_Exception(string firstName)
        {
            var useCase = new RegisterCustomerUseCase(null);
            Action register = () => useCase.Register(new CustomerRegistration(firstName, "Flintstone", "fred@flintstones.net"));
            register.Should().ThrowExactly<MissingFirstName>()
                .WithMessage("Missing first name.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(" \r\n  ")]
        public void Given_No_LastName_When_Call_Register_Then_Throw_MissingLastName_Exception(string lastName)
        {
            var useCase = new RegisterCustomerUseCase(null);
            Action register = () => useCase.Register(new CustomerRegistration("Fred", lastName, "fred@flintstones.net"));
            register.Should().ThrowExactly<MissingLastName>()
                .WithMessage("Missing last name.");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("   ")]
        [InlineData(" \r\n  ")]
        public void Given_No_EmailAddress_When_Call_Register_Then_Throw_MissingEmailAddress_Exception(string emailAddress)
        {
            var useCase = new RegisterCustomerUseCase(null);
            Action register = () => useCase.Register(new CustomerRegistration("Fred", "Flintstone", emailAddress));
            register.Should().ThrowExactly<MissingEmailAddress>()
                .WithMessage("Missing email address.");
        }

        [Theory]
        [InlineData("Fred", "Flintstone", "fred@flintstones.net")]
        [InlineData("Barney", "Rubble", "barney@rubbles.rock")]
        [InlineData("Wilma", "Flintstone", "wilma@flintstones.net")]
        public void When_Call_Register_Then_Try_Lookup_Customer_By_EmailAddress(string firstName,
            string lastName, string emailAddress)
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            var useCase = new RegisterCustomerUseCase(mockCustomerRepo.Object);
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
            useCase.Register(registration);
            mockCustomerRepo.Verify(x => x.GetCustomer(emailAddress));
        }

        [Theory]
        [InlineData("Fred", "Flintstone", "fred@flintstones.net")]
        [InlineData("Barney", "Rubble", "barney@rubbles.rock")]
        [InlineData("Wilma", "Flintstone", "wilma@flintstones.net")]
        public void Given_Customer_Already_Exists_When_Call_Register_Then_Throw_DuplicateCustomerEmailAddress_Exception(string firstName,
            string lastName, string emailAddress)
        {
            var customer = new Customer(Guid.NewGuid(), firstName, lastName, emailAddress);
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockCustomerRepo.Setup(x => x.GetCustomer(It.IsAny<string>()))
                .Returns(customer);
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
            var useCase = new RegisterCustomerUseCase(mockCustomerRepo.Object);
            Action register = () => useCase.Register(registration);
            register.Should().ThrowExactly<DuplicateCustomerEmailAddress>()
                .WithMessage($"Customer with email address '{emailAddress}' already exists.");
        }

        [Theory]
        [InlineData("Fred", "Flintstone", "fred@flintstones.net")]
        [InlineData("Barney", "Rubble", "barney@rubbles.rock")]
        [InlineData("Wilma", "Flintstone", "wilma@flintstones.net")]
        public void Given_New_Customer_When_Call_Register_Then_Save_Customer_To_Repository(string firstName,
            string lastName, string emailAddress)
        {
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockCustomerRepo.Setup(x => x.GetCustomer(It.IsAny<string>())).Returns((Customer)null);
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
            var useCase = new RegisterCustomerUseCase(mockCustomerRepo.Object);
            var customer = useCase.Register(registration);
            mockCustomerRepo.Verify(x => x.SaveCustomer(customer));
        }
    }
}