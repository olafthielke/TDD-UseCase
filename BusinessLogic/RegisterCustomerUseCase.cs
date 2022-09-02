﻿using System;
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


        public void Register(CustomerRegistration registration)
        {
            Validate(registration);

            var customer = new Customer(Guid.NewGuid(), registration.FirstName, registration.LastName, registration.EmailAddress);

            Repository.SaveCustomer(customer);
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
