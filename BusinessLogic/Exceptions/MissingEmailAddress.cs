using System;

namespace BusinessLogic.Exceptions
{
    public class MissingEmailAddress : Exception
    {
        public MissingEmailAddress()
            : base("Missing email address.")
        { }
    }
}
