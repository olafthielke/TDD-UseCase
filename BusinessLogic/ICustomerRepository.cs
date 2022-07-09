namespace BusinessLogic
{
    public interface ICustomerRepository
    {
        Customer GetCustomer(string emailAddress);
    }
}
