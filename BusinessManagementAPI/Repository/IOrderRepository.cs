using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetOrders();
        Task<Order> GetOrder(int id);
        Task<bool> DeleteOrder(int id);
        Task<bool> UpdateOrder(Order order);
        Task<bool> CreateOrder(OrderDTO orderDTO);
        Task<bool> CompleteOrder(int id);
        Task<bool> UpdateOrderGroup(OrderGroupDTO orderGroupDTO);
        Task<Order> UpdateOrderGroup(OrderDTO orderDTO);
    }
}
