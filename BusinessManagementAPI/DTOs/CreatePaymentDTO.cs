using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class CreatePaymentDTO
    {
        public int Id { get; set; }

        public float Amount { get; set; }

        public string Type { get; set; }
        public int OrderId { get; set; }

        public static Payment ToPayment(CreatePaymentDTO createPaymentDTO)
        {
            return new Payment
            {
                Id = createPaymentDTO.Id,
                Amount = createPaymentDTO.Amount,
                Type = createPaymentDTO.Type,
                OrderId = createPaymentDTO.OrderId,
            };
        }
    }
}