using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int? PhoneNumber { get; set; }
        public static Customer ToCustomer(CustomerDTO customerDTO)
        {
            return new Customer { Id = customerDTO.Id, FullName = customerDTO.FullName, PhoneNumber = customerDTO.PhoneNumber };
        }
    }

    
}
