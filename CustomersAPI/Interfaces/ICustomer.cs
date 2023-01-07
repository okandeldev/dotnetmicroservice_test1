using CustomersAPI.Models;

namespace CustomersAPI.Interfaces
{
    public interface ICustomer
    {
        Task addCustomer(Customer customer);
    }
}
