using System;

namespace BusinessLogic.Exceptions
{
    public class MissingFirstName : Exception
    {
        public MissingFirstName()
            : base("Missing first name.")
        { }
    }
}
