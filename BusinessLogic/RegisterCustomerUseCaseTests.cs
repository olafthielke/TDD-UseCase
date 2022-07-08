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
    }
}
