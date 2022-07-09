using System;
using Xunit;
using FluentAssertions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCaseTests
    {
        [Fact]
        public void Given_No_Customer_When_Call_Register_Then_Throw_MissingCustomer_Exception()
        {
            var useCase = new RegisterCustomerUseCase();
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
            var useCase = new RegisterCustomerUseCase();
            Action register = () => useCase.Register(new Customer(firstName, "Flintstone"));
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
            var useCase = new RegisterCustomerUseCase();
            Action register = () => useCase.Register(new Customer("Fred", lastName));
            register.Should().ThrowExactly<MissingLastName>()
                .WithMessage("Missing last name.");
        }

        [Fact]
        public void Given_No_EmailAddress_When_Call_Register_Then_Throw_MissingEmailAddress_Exception()
        {
            var useCase = new RegisterCustomerUseCase();
            Action register = () => useCase.Register(new Customer("Fred", "Flintstone", null));
            register.Should().ThrowExactly<MissingEmailAddress>();
        }
    }


    public class RegisterCustomerUseCase
    {
        public void Register(Customer customer)
        {
            if (customer == null)
                throw new MissingCustomer();
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new MissingFirstName();
            throw new MissingLastName();
        }
    }

    public class Customer
    {
        public string FirstName { get; }

        public Customer(string firstName, string lastName)
        {
            FirstName = firstName;
        }
    }

    public class MissingCustomer : Exception
    {
        public MissingCustomer() 
            : base("Missing customer.") 
        { }
    }

    public class MissingFirstName : Exception
    {
        public MissingFirstName() 
            : base("Missing first name.")
        { }
    }

    public class MissingLastName : Exception
    {
        public MissingLastName() 
            : base("Missing last name.") 
        { }
    }
}
