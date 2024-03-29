﻿using BusinessManagementAPI.DTOs;
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
            foreach (var product in order.Products)
            {
                product.CategoryId = product.Category!.Id;
                product.Category = null;
            }
            _ordersContext.Orders.Add(order);


            foreach (var product in order.Products!)
                product.Category = await _ordersContext.Categories.FirstAsync(x => x.Id == product.CategoryId);
            

            if (await _ordersContext.SaveChangesAsync() > 0)
                return order;
            else
                return null!;
        }

        public async Task<bool> DeleteOrder(int id)
        {
            var order = _ordersContext.Orders.Include(x => x.Payments).Include(x => x.Products).Include(x => x.Customer).Where(x => x.Id == id).ToList();

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
        
        public async Task<IEnumerable<Order>> GetActiveOrders()
        {
            return await _ordersContext.Orders.Where(x => x.Status == false).Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ThenInclude(p => p.Category).ToListAsync();
        }
        public async Task<IEnumerable<Order>> GetInactiveOrders()
        {
            return await _ordersContext.Orders.Where(x => x.Status == true).Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ThenInclude(p => p.Category).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetOrders(bool status)
        {
            return await _ordersContext.Orders.Include(x => x.Customer).Include(x => x.Payments).Include(p => p.Products).ThenInclude(p => p.Category).Where(x => x.Status == status).ToListAsync();
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

        public async Task<Order> UpdateOrderGroup(OrderDTO orderDTO)
        {
            var order = OrderDTO.ToOrder(orderDTO);
            var tracking = _ordersContext.Orders.Update(order);

            if (await _ordersContext.SaveChangesAsync() > 0)
            {
                await UpdateOrderPriceAndBalance(order.Id);
                return order;
            }
            else
            {
                return new Order { };
            }
        }

        public async Task<bool> CheckOrderExists(int id)
        {
            return await _ordersContext.Orders.AnyAsync(order => order.Id == id);
        }

        public IEnumerable<CalenderDTO> GetOrdersForCalender()
        {
            IEnumerable<CalenderDTO> calenderDTOs = _ordersContext.Orders.Include(x => x.Products).Where(x => x.Status == false).Select(x => new CalenderDTO
            {
                Id = x.Id,
                NumProducts = x.Products.Count,
                FulfillmentDate = x.FulfillmentDate,
            });

            return calenderDTOs;
        }

        public async Task<IEnumerable<Order>> SearchOrdersByName(string name)
        {
            name = name.ToLower().Trim();
            return await _ordersContext.Orders
                .Include(x => x.Customer)
                .Include(x => x.Products)
                .Include(x => x.Payments)
                .Where(x => x.Customer.FullName.ToLower().Replace(" ", "").Contains(name)).ToListAsync();
        }

        public async Task<bool> UpdateOrderPriceAndBalance(int id)
        {
            var order = _ordersContext.Orders.Include(x => x.Products).Include(x => x.Payments).Where(x => x.Id == id).ToList().First();
            order.Total = order.Balance = 0;
            order.Total += order.DeliveryFee;
            order.Total += order.Products.Sum(x => x.Price);
            order.Balance = order.Total;
            order.Balance -= order.Payments.Sum(x => x.Amount);
            order.Balance = (float)Math.Round(order.Balance, 2);
            order.Total = (float)Math.Round(order.Total, 2);

            return await _ordersContext.SaveChangesAsync() > 0;
        }
    }
}