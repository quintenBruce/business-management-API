using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace BusinessManagementAPI.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrdersContext _ordersContext;

        public OrderRepository(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<bool> CreateOrder(Order order)
        {
            _ordersContext.Orders.Add(order);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = _ordersContext.Orders.Find(id);
            _ordersContext.Remove(order);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _ordersContext.Orders.Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _ordersContext.Orders.Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ToListAsync();
        }

        public async Task<bool> UpdateOrder(Order order)
        {
            _ordersContext.Orders.Update(order);
            return await _ordersContext.SaveChangesAsync() == 1;
        }
    }
}