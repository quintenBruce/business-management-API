using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class PaymentDTO
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Type { get; set; } = null!;
        
        public static Payment ToPayment(PaymentDTO p)
        {
            return new Payment { Id = p.Id, Amount = p.Amount, Type = p.Type};
        }

        public static List<Payment> ToPaymentList(List<PaymentDTO> paymentDTOs)
        {
            return (List<Payment>)paymentDTOs.Select(x => PaymentDTO.ToPayment(x));
        }
    }
}
