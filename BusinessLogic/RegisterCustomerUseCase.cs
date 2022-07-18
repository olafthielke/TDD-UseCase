using BusinessLogic.Exceptions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCase
    {
        private ICustomerRepository Repository { get; }

        public RegisterCustomerUseCase(ICustomerRepository repository)
        {
            Repository = repository;
        }


        public void Register(Customer customer)
        {
            Validate(customer);

            Repository.GetCustomer("fred@flintstones.net");
        }

        private static void Validate(Customer customer)
        {
            if (customer == null)
                throw new MissingCustomer();
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new MissingFirstName();
            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new MissingLastName();
            if (string.IsNullOrWhiteSpace(customer.EmailAddress))
                throw new MissingEmailAddress();
        }
    }
}
