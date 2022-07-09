using System;

namespace BusinessLogic.Exceptions
{
    public class MissingLastName : Exception
    {
        public MissingLastName()
            : base("Missing last name.")
        { }
    }
}
