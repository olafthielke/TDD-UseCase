using System;

namespace BusinessLogic.Exceptions
{
    public class DuplicateCustomerEmailAddress : Exception
    {
        public DuplicateCustomerEmailAddress()
            : base("Customer with email address 'fred@flintstones.net' already exists.")
        { }
    }
}