using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public interface IPaymentRepository
    {
        Task<IEnumerable<Payment>> GetPayments();
        Task<Payment> GetPayment(int id);
        Task<bool> DeletePayment(int id);
        Task<bool> UpdatePayment(Payment payment);
        Task<bool> CreatePayment(Payment payment);
    }
}
