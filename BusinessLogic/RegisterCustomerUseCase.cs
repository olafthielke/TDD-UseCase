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


        public void Register(CustomerRegistration registration)
        {
            Validate(registration);

            // Somewhere here we are converting the registration to a customer (with customer id)

            //Repository.SaveCustomer(customer);
        }

        private void Validate(CustomerRegistration registration)
        {
            if (registration == null)
                throw new MissingCustomerRegistration();
            registration.Validate();
            var existCust = Repository.GetCustomer(registration.EmailAddress);
            if (existCust != null)
                throw new DuplicateCustomerEmailAddress(registration.EmailAddress);
        }
    }
}
