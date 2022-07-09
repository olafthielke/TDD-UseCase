namespace BusinessLogic
{
    public class Customer
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Customer(string firstName, string lastName, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
