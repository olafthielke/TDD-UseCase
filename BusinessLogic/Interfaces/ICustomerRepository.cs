using BusinessLogic.Entities;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface ICustomerRepository
    {
        Task<Customer> GetCustomer(string emailAddress);

        Task SaveCustomer(Customer customer);
    }
}
