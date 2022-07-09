namespace BusinessLogic
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool WasGetCustomerCalled;
        public string PassedInEmailAddress;

        public Customer CustomerToBeReturned;

        public MockCustomerRepository(Customer customerToBeReturned)
        {
            CustomerToBeReturned = customerToBeReturned;
        }


        public Customer GetCustomer(string emailAddress)
        {
            WasGetCustomerCalled = true;
            PassedInEmailAddress = emailAddress;

            return CustomerToBeReturned;
        }
    }
}
