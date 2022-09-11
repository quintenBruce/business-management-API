using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<IEnumerable<Order>> GetOrders(bool status);
        Task<Order> GetOrder(int id);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrder(Order order);
        Task<Order> CreateOrder(OrderDTO orderDTO);
        Task<bool> CompleteOrder(int id);
        
        Task<Order> UpdateOrderGroup(OrderDTO orderDTO);
        
        Task<bool> CheckOrderExists(int id);
        Task<IEnumerable<CalenderDTO>> GetOrdersForCalender();
        Task<IEnumerable<Order>> SearchOrdersByName(string name);
        Task<bool> UpdateOrderPriceAndBalance(int id);
    }
}
