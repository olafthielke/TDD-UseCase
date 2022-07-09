namespace BusinessLogic
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool WasGetCustomerCalled;
        public string PassedInEmailAddress;

        public void GetCustomer(string emailAddress)
        {
            WasGetCustomerCalled = true;
            PassedInEmailAddress = emailAddress;
        }
    }
}
