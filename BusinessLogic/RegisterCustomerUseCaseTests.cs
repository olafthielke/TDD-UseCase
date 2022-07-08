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

        [Fact]
        public void Given_No_FirstName_When_Call_Register_Then_Throw_MissingFirstName_Exception()
        {
            var useCase = new RegisterCustomerUseCase();
            Action register = () => useCase.Register(new Customer(null));
            register.Should().ThrowExactly<MissingFirstName>();
        }
    }


    public class RegisterCustomerUseCase
    {
        public void Register(object customer)
        {
            throw new MissingCustomer();
        }
    }


    public class MissingCustomer : Exception
    {
        public MissingCustomer() 
            : base("Missing customer.") 
        { }
    }
}
