﻿using System;

namespace BusinessLogic.Entities
{
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }
        public string EmailAddress { get; }
        public Guid Id { get; }

        public Customer(Guid id, string firstName, string lastName, string emailAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = emailAddress;
        }
    }
}
