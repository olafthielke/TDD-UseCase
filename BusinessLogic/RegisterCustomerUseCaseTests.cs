using System;
using Xunit;
using FluentAssertions;
using BusinessLogic.Exceptions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCaseTests
    {
        [Fact]
        public void Given_No_Customer_When_Call_Register_Then_Throw_MissingCustomer_Exception()
        {
            var useCase = SetupUseCase();
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
            var useCase = SetupUseCase();
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
            var useCase = SetupUseCase();
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
            var useCase = SetupUseCase();
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
            var useCase = SetupUseCase();
            var customer = new Customer(firstName, lastName, emailAddress);
            useCase.Register(customer);
            VerifyRepoCallToGetCustomer(emailAddress, useCase);
        }

        [Theory]
        [InlineData("Fred", "Flintstone", "fred@flintstones.net")]
        [InlineData("Barney", "Rubble", "barney@rubbles.rock")]
        [InlineData("Wilma", "Flintstone", "wilma@flintstones.net")]
        public void Given_Customer_Already_Exists_When_Call_Register_Then_Throw_DuplicateCustomerEmailAddress_Exception(string firstName,
            string lastName, string emailAddress)
        {
            var customer = new Customer(firstName, lastName, emailAddress);
            var useCase = SetupUseCase(customer);
            Action register = () => useCase.Register(customer);
            register.Should().ThrowExactly<DuplicateCustomerEmailAddress>()
                .WithMessage($"Customer with email address '{emailAddress}' already exists.");
        }


        private static RegisterCustomerUseCase SetupUseCase(Customer customerToBeReturned = null)
        {
            var mockCustomerRepo = new MockCustomerRepository(customerToBeReturned);
            return new RegisterCustomerUseCase(mockCustomerRepo);
        }


        private static void VerifyRepoCallToGetCustomer(string emailAddress, RegisterCustomerUseCase useCase)
        {
            var mockCustomerRepo = (MockCustomerRepository)useCase.Repository;
            
            mockCustomerRepo.WasGetCustomerCalled.Should().BeTrue();
            mockCustomerRepo.PassedInEmailAddress.Should().Be(emailAddress);
        }
    }
}