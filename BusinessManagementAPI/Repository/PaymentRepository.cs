using AutoMapper;
using BusinessManagementAPI.DTOs;
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

        public async Task<bool> CreatePayments(List<CreatePaymentDTO> payments)
        {
            _ordersContext.Payments.AddRange(payments.Select(x => CreatePaymentDTO.ToPayment(x)).ToList());
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePayment(int id)
        {
            var payment = await _ordersContext.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
            _ordersContext.Remove(payment);
            return await _ordersContext.SaveChangesAsync() > 0;
        }

        public async Task<Payment> GetPayment(int id) => await _ordersContext.Payments.FindAsync(id);

        public async Task<IEnumerable<Payment>> GetPayments(int id)
        {
            return await _ordersContext.Payments
                .Include(x => x.Order)
                .Where(p => (id > 0) ? p.OrderId == id : true)
                .Select(x => new Payment
                {
                    OrderId = x.OrderId,
                    Amount = x.Amount,
                    Id = x.Id,
                    Type = x.Type,
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PaymentDTO>> UpdatePayment(List<PaymentDTO> payments)
        {
   



            List<Payment> paymentsList = (List<Payment>)payments.Select(x => PaymentDTO.ToPayment(x)).ToList();
            _ordersContext.Payments.UpdateRange(paymentsList);
            paymentsList.ForEach(q =>
            {
                _ordersContext.Entry(q).Property(x => x.OrderId).IsModified = false;

            });


            int status = await _ordersContext.SaveChangesAsync();
            if (status > 0)
                return payments;
            else
                return new List<PaymentDTO> { };

        }
    }
}