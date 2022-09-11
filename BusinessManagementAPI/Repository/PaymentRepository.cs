using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly OrdersContext _ordersContext;
        private readonly IOrderRepository _orderRepository;

        public PaymentRepository(OrdersContext ordersContext, IOrderRepository orderRepository)
        {
            _ordersContext = ordersContext;
            _orderRepository = orderRepository;
        }

        public async Task<bool> CreatePayments(List<CreatePaymentDTO> payments)
        {
            var paymentsList = payments.Select(x => CreatePaymentDTO.ToPayment(x)).ToList();
            _ordersContext.Payments.AddRange(paymentsList);
            var order = _ordersContext.Orders.Where(x => x.Id == payments.ElementAt(0).OrderId).First();
            paymentsList.ForEach(x =>
            {
                order.Total -= x.Amount;
            });
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeletePayment(int id)
        {
            var payment = await _ordersContext.Payments.Where(x => x.Id == id).FirstOrDefaultAsync();
            _ordersContext.Remove(payment);

            if (await _ordersContext.SaveChangesAsync() > 0)
            {
                var order = _ordersContext.Orders.First(x => x.Id == payment.OrderId);
                order.Balance += payment!.Amount;
                
                return true;
            }
            else
                return false;
            
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

        public async Task<IEnumerable<PaymentDTO>> UpdatePayment(List<PaymentDTO> payments, int orderId)
        {
            List<Payment> paymentsList = (List<Payment>)payments.Select(x => PaymentDTO.ToPayment(x)).ToList();
            _ordersContext.Payments.UpdateRange(paymentsList);
            paymentsList.ForEach(q =>
            {
                _ordersContext.Entry(q).Property(x => x.OrderId).IsModified = false;
            });

            int status = await _ordersContext.SaveChangesAsync();
            if (status > 0)
            {
                await _orderRepository.UpdateOrderPriceAndBalance(orderId);
                return payments;

            }
            else
                return new List<PaymentDTO> { };
        }
    }
}