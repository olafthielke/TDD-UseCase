using System;
using System.Threading.Tasks;
using Xunit;
using FluentAssertions;
using BusinessLogic.Exceptions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCaseTests
    {
        [Fact]
        public async Task Given_No_CustomerRegistration_When_Call_Register_Then_Throw_MissingCustomerRegistration_Exception()
        {
            var useCase = SetupUseCase();
            Func<Task> register = async () => { await useCase.Register(null); };
            await register.Should().ThrowExactlyAsync<MissingCustomerRegistration>()
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
            var useCase = SetupUseCase();
            var registration = new CustomerRegistration(firstName, "Flintstone", "fred@flintstones.net");
            Action register = () => useCase.Register(registration);
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
            var registration = new CustomerRegistration("Fred", lastName, "fred@flintstones.net");
            Action register = () => useCase.Register(registration);
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
            var registration = new CustomerRegistration("Fred", "Flintstone", emailAddress);
            Action register = () => useCase.Register(registration);
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
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
            useCase.Register(registration);
            VerifyRepoCallToGetCustomer(emailAddress, useCase);
        }

        [Theory]
        [InlineData("Fred", "Flintstone", "fred@flintstones.net")]
        [InlineData("Barney", "Rubble", "barney@rubbles.rock")]
        [InlineData("Wilma", "Flintstone", "wilma@flintstones.net")]
        public void Given_Customer_Already_Exists_When_Call_Register_Then_Throw_DuplicateCustomerEmailAddress_Exception(string firstName,
            string lastName, string emailAddress)
        {
            var useCase = SetupUseCase(new Customer(Guid.NewGuid(), firstName, lastName, emailAddress));
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
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
            var useCase = SetupUseCase();
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
            useCase.Register(registration);
            VerifyRepoCallToSaveCustomer(registration, useCase);
        }

        [Theory]
        [InlineData("Fred", "Flintstone", "fred@flintstones.net")]
        [InlineData("Barney", "Rubble", "barney@rubbles.rock")]
        [InlineData("Wilma", "Flintstone", "wilma@flintstones.net")]
        public void Given_New_Customer_When_Call_Register_Then_Return_New_Customer(string firstName,
            string lastName, string emailAddress)
        {
            var useCase = SetupUseCase();
            var registration = new CustomerRegistration(firstName, lastName, emailAddress);
            var customer = useCase.Register(registration);
            VerifyCustomer(customer, useCase);
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

        private static void VerifyRepoCallToSaveCustomer(CustomerRegistration registration,
            RegisterCustomerUseCase useCase)
        {
            var mockCustomerRepo = (MockCustomerRepository)useCase.Repository;
            mockCustomerRepo.WasSaveCustomerCalled.Should().BeTrue();
            VerifyCustomer(registration, mockCustomerRepo);
        }

        private static void VerifyCustomer(CustomerRegistration registration, MockCustomerRepository mockCustomerRepo)
        {
            mockCustomerRepo.PassedInCustomer.Id.Should().NotBeEmpty();
            mockCustomerRepo.PassedInCustomer.FirstName.Should().Be(registration.FirstName);
            mockCustomerRepo.PassedInCustomer.LastName.Should().Be(registration.LastName);
            mockCustomerRepo.PassedInCustomer.EmailAddress.Should().Be(registration.EmailAddress);
        }

        private static void VerifyCustomer(Customer customer, RegisterCustomerUseCase useCase)
        {
            var mockCustomerRepo = (MockCustomerRepository)useCase.Repository;
            customer.Should().BeEquivalentTo(mockCustomerRepo.PassedInCustomer);
        }
    }
}