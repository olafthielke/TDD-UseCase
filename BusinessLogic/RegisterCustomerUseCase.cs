using BusinessLogic.Exceptions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCase
    {
        public ICustomerRepository Repository { get; }

        public RegisterCustomerUseCase(ICustomerRepository repository)
        {
            Repository = repository;
        }


        public void Register(Customer customer)
        {
            Validate(customer);

            Repository.SaveCustomer(customer);
        }

        private void Validate(Customer customer)
        {
            if (customer == null)
                throw new MissingCustomer();
            customer.Validate();
            var existCust = Repository.GetCustomer(customer.EmailAddress);
            if (existCust != null)
                throw new DuplicateCustomerEmailAddress(customer.EmailAddress);
        }
    }
}
