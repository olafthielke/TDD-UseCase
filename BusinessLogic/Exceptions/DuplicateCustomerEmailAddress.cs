using System;

namespace BusinessLogic.Exceptions
{
    public class DuplicateCustomerEmailAddress : Exception
    {
        public DuplicateCustomerEmailAddress(string emailAddress)
            : base($"Customer with email address '{emailAddress}' already exists.")
        { }
    }
}