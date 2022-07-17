using System;
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


        public Customer Register(CustomerRegistration registration)
        {
            Validate(registration);
            var customer = registration.ToCustomer();
            Repository.SaveCustomer(customer);
            return customer;
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
