namespace BusinessLogic
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool WasGetCustomerCalled;
        public bool WasSaveCustomerCalled;
        public string PassedInEmailAddress;
        public Customer PassedInCustomer;

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

        public void SaveCustomer(Customer customer)
        {
            WasSaveCustomerCalled = true;
            PassedInCustomer = customer;
        }
    }
}
