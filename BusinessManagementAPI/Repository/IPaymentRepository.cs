using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPayments(int id);
        Task<Payment> GetPayment(int id);
        Task<bool> DeletePayment(int id);
        Task<IEnumerable<PaymentDTO>> UpdatePayment(List<PaymentDTO> payments);
        Task<bool> CreatePayments(List<CreatePaymentDTO> payments);
    }
}
