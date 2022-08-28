using BusinessManagementAPI.DTOs;
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

        public async Task<bool> CreateOrder(OrderDTO orderDTO)
        {
            var customer = OrderDTO.ToCustomer(orderDTO);
            _ordersContext.Customers.Add(customer);

            var order = OrderDTO.ToOrder(orderDTO);
            _ordersContext.Orders.Add(order);
            order.Customer = customer;
            return await _ordersContext.SaveChangesAsync() == 2;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = _ordersContext.Orders.Find(id);
            _ordersContext.Remove(order);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _ordersContext.Orders.Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ThenInclude(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
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

        public async Task<bool> CompleteOrder(int id)
        {
            var order = new Order { Id = id, Status = true, CompletionDate = DateTime.Now };
            var tracking = _ordersContext.Orders.Attach(order);
            tracking.Property(x => x.Status).IsModified = true;
            tracking.Property(x => x.CompletionDate).IsModified = true;
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> UpdateOrderGroup(OrderGroupDTO orderGroupDTO)
        {
            _ordersContext.Orders.Update(OrderGroupDTO.ToOrder(orderGroupDTO));
            return await _ordersContext.SaveChangesAsync() > 0;
        }

        public async Task<Order> UpdateOrderGroup(OrderDTO orderDTO)
        {
            var order = OrderDTO.ToOrder(orderDTO);
            var tracking = _ordersContext.Orders.Update(order);


            if (await _ordersContext.SaveChangesAsync() > 0)
            {
                return order;
            }
            else
            {
                return new Order { };
            }
        }
    }
}