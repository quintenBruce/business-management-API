using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomersController : Controller
    {
        private ICustomerRepository _customerRepository;

        public CustomersController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomer(int id)
        {
            Customer customer = await _customerRepository.GetCustomer(id);
            return customer is not null ? Ok(customer) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomers()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetCustomers();
            return Utilities.IsAny(customers) ? Ok(customers) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(Customer customer)
        {
            bool status = await _customerRepository.UpdateCustomer(customer);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            bool status = await _customerRepository.DeleteCustomer(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
