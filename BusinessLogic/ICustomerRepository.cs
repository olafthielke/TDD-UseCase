namespace BusinessLogic
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string emailAddress);

        void SaveCustomer(Customer customer);
    }
}