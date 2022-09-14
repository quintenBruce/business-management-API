using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.Repository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetCustomers();

        Task<Customer> GetCustomer(int id);

        Task<bool> DeleteCustomer(int id);

        Task<bool> UpdateCustomer(Customer customer);

        Task<bool> CreateCustomer(Customer customer);
    }
}