using System;
using FluentAssertions;
using Xunit;

namespace BusinessLogic
{
    public class RegisterCustomerUseCaseTests
    {
        [Fact]
        public void Can_Instantiate_UseCase()
        {
            var useCase = new RegisterCustomerUseCase();
        }

        //[Fact]
        //public void Can_Call_Register()
        //{
        //    var useCase = new RegisterCustomerUseCase();
        //    useCase.Register();
        //}

        [Fact]
        public void Given_No_Customer_When_Call_Register_Then_Throw_MissingCustomer_Exception()
        {
            var useCase = new RegisterCustomerUseCase();
            Action register = () => useCase.Register(null);
            register.Should().ThrowExactly<MissingCustomer>();
        }
    }


    public class RegisterCustomerUseCase
    {
        public void Register(object customer)
        {

        }
    }

    public class MissingCustomer : Exception
    {

    }
}
