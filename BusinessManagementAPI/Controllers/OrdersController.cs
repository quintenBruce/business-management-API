using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : Controller
    {
        private IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetOrder(int id)
        {
            Order order = await _orderRepository.GetOrder(id);
            return order is not null ? Ok(order) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetOrders()
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrders();
            return Utilities.IsAny(orders) ? Ok(orders) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrder(Order order)
        {
            bool status = await _orderRepository.UpdateOrder(order);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool status = await _orderRepository.DeleteOrder(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}