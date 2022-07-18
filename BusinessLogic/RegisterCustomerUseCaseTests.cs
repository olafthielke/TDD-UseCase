using System;
using Xunit;
using FluentAssertions;
using BusinessLogic.Exceptions;
using Moq;

namespace BusinessLogic
{
    public class RegisterCustomerUseCaseTests
    {
        [Fact]
        public void Given_No_Customer_When_Call_Register_Then_Throw_MissingCustomer_Exception()
        {
            var useCase = new RegisterCustomerUseCase(null);
            Action register = () => useCase.Register(null);
            register.Should().ThrowExactly<MissingCustomer>()
                             .WithMessage("Missing customer.");
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
            Action register = () => useCase.Register(new Customer(firstName, "Flintstone", "fred@flintstones.net"));
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
            Action register = () => useCase.Register(new Customer("Fred", lastName, "fred@flintstones.net"));
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
            Action register = () => useCase.Register(new Customer("Fred", "Flintstone", emailAddress));
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
            var customer = new Customer(firstName, lastName, emailAddress);
            useCase.Register(customer);
            mockCustomerRepo.Verify(x => x.GetCustomer(emailAddress));
        }

        [Fact]
        public void Given_Customer_Already_Exists_When_Call_Register_Then_Throw_DuplicateCustomerEmailAddress_Exception()
        {
            var customer = new Customer("Fred", "Flintstone", "fred@flintstones.net");
            var mockCustomerRepo = new Mock<ICustomerRepository>();
            mockCustomerRepo.Setup(x => x.GetCustomer(It.IsAny<string>()))
                .Returns(customer);
            var useCase = new RegisterCustomerUseCase(mockCustomerRepo.Object);
            Action register = () => useCase.Register(customer);
            register.Should().ThrowExactly<DuplicateCustomerEmailAddress>();
        }
    }
}