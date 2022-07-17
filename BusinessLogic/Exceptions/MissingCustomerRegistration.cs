using System;

namespace BusinessLogic.Exceptions
{
    public class MissingCustomerRegistration : Exception
    {
        public MissingCustomerRegistration()
            : base("Missing customer registration.")
        { }
    }
}
