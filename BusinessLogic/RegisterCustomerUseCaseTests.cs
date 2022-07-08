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

        [Fact]
        public void Can_Call_Register()
        {
            var useCase = new RegisterCustomerUseCase();
            useCase.Register();
        }
    }


    public class RegisterCustomerUseCase
    {
        public void Register()
        {

        }
    }
}
