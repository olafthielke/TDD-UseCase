using BusinessLogic.Exceptions;

namespace BusinessLogic
{
    public class RegisterCustomerUseCase
    {
        public void Register(Customer customer)
        {
            if (customer == null)
                throw new MissingCustomer();
            if (string.IsNullOrWhiteSpace(customer.FirstName))
                throw new MissingFirstName();
            if (string.IsNullOrWhiteSpace(customer.LastName))
                throw new MissingLastName();
            throw new MissingEmailAddress();
        }
    }
}
