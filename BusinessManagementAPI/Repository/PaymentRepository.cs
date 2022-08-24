using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrdersContext _ordersContext;

        public PaymentRepository(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }
        public async Task<bool> CreatePayment(Payment payment)
        {
            _ordersContext.Payments.Add(payment);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePayment(int id)
        {
            var payment = _ordersContext.Payments.Find(id);
            _ordersContext.Remove(payment);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<Payment> GetPayment(int id) => await _ordersContext.Payments.FindAsync(id);

        public async Task<IEnumerable<Payment>> GetPayments() => await _ordersContext.Payments.Include(x => x.Order).ToListAsync();

        public async Task<bool> UpdatePayment(Payment payment)
        {
            _ordersContext.Payments.Update(payment);
            return await _ordersContext.SaveChangesAsync() == 1;
        }
    }
}
