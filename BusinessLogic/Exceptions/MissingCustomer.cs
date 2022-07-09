using System;

namespace BusinessLogic.Exceptions
{
    public class MissingCustomer : Exception
    {
        public MissingCustomer()
            : base("Missing customer.")
        { }
    }
}
