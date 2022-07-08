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

        [Fact]
        public void Given_No_LastName_When_Call_Register_Then_Throw_MissingLastName_Exception()
        {
            var useCase = new RegisterCustomerUseCase();
            Action register = () => useCase.Register(new Customer("Fred", null));
            register.Should().ThrowExactly<MissingLastName>();
        }
    }


    public class RegisterCustomerUseCase
    {
        public void Register(object customer)
        {
            if (customer == null)
                throw new MissingCustomer();
            throw new MissingFirstName();
        }
    }

    public class Customer
    {
        public Customer(string firstName, string lastName)
        { }
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

    }
}
