using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class CalenderDTO
    {
        public int Id { get; set; }
        public int NumProducts { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public CalenderDTO ToCalenderDTO(Order order)
        {
            return new CalenderDTO
            {
                Id = order.Id,
                NumProducts = order.Products.Count,
                FulfillmentDate = order.FulfillmentDate,
            };
        }
    }

    
}
