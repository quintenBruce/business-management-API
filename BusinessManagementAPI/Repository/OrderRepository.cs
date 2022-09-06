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

        public async Task<Order> CreateOrder(OrderDTO orderDTO)
        {
            var order = OrderDTO.ToOrder(orderDTO);
            _ordersContext.Orders.Add(order);
            _ordersContext.Entry(order.Products.ElementAt(0).Category).State = EntityState.Detached;
            if (await _ordersContext.SaveChangesAsync() > 0)
                return order;
            else
                return new Order { };
            
            
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = _ordersContext.Orders.Include(x => x.Payments).Include(x=> x.Products).Include(x => x.Customer).Where(x => x.Id == id).ToList();
            
            _ordersContext.Remove(order.ElementAt(0));
            _ordersContext.Remove(order.ElementAt(0).Customer);

            return await _ordersContext.SaveChangesAsync() > 0;
        }

        public async Task<Order> GetOrder(int id)
        {
            return await _ordersContext.Orders.Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ThenInclude(p => p.Category).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Order>> GetOrders()
        {
            return await _ordersContext.Orders.Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ThenInclude(p => p.Category).ToListAsync();
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

        public void UpdateOrderGroup3()
        {
            List<Customer> customers = _ordersContext.Customers.Include(c => c.Orders).ToList();
            customers = customers.Where(c => c.Orders.Count < 1).ToList();
            customers.ForEach(x =>
            {
                _ordersContext.Remove(x);
            });

            var status = _ordersContext.SaveChanges();
            var lkasdf = 9;
        }

        public async Task<bool> CheckOrderExists(int id)
        {
            return await _ordersContext.Orders.AnyAsync(order => order.Id == id);
        }

        public async Task<IEnumerable<CalenderDTO>> GetOrdersForCalender()
        {
            IEnumerable<CalenderDTO> calenderDTOs =  _ordersContext.Orders.Include(x => x.Products).Where(x => x.Status == false).Select(x => new CalenderDTO
            {
                Id = x.Id,
                NumProducts = x.Products.Count,
                FulfillmentDate = x.FulfillmentDate,
            });

            return calenderDTOs;


        }
    }
}