using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly OrdersContext _ordersContext;

        public CustomerRepository(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<bool> CreateCustomer(Customer customer)
        {
            _ordersContext.Customers.Add(customer);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteCustomer(int id)
        {
            var customer = _ordersContext.Customers.Find(id);
            _ordersContext.Remove(customer);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<Customer> GetCustomer(int id)
        {
            return await _ordersContext.Customers.FindAsync(id);
        }

        public async Task<IEnumerable<Customer>> GetCustomers()
        {
            return await _ordersContext.Customers.ToListAsync();
        }

        public async Task<bool> UpdateCustomer(Customer customer)
        {
            _ordersContext.Customers.Update(customer);
            return await _ordersContext.SaveChangesAsync() == 1;
        }
    }
}