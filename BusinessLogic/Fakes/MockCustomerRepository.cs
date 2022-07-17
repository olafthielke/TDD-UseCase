using BusinessLogic.Entities;
using BusinessLogic.Interfaces;
using System.Threading.Tasks;

namespace BusinessLogic.Fakes
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


        public async Task<Customer> GetCustomer(string emailAddress)
        {
            await Task.CompletedTask;

            WasGetCustomerCalled = true;
            PassedInEmailAddress = emailAddress;

            return CustomerToBeReturned;
        }

        public async Task SaveCustomer(Customer customer)
        {
            await Task.CompletedTask;

            WasSaveCustomerCalled = true;
            PassedInCustomer = customer;
        }
    }
}
