namespace BusinessLogic
{
    public class MockCustomerRepository : ICustomerRepository
    {
        public bool WasGetCustomerCalled;

        public void GetCustomer()
        {
            WasGetCustomerCalled = true;
        }
    }
}
