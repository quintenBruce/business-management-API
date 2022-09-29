using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Filters;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BusinessManagementAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrdersController : ControllerBase
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

            return order is not null ? Ok(order) : StatusCode(StatusCodes.Status500InternalServerError);
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

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            bool status = await _orderRepository.DeleteOrder(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CompleteOrder(int id)
        {
            return await _orderRepository.CompleteOrder(id) ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO orderDTO)
        {
                Order order = await _orderRepository.CreateOrder(orderDTO);
            return order is not null ? Ok(order) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderGroup2(OrderDTO orderDTO) //accept an order domain model because updating requires ids and data
        {
            Order order = await _orderRepository.UpdateOrderGroup(orderDTO);
            return order is not null ? Ok(order) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> CheckOrderExists(int id)
        {
            return await _orderRepository.CheckOrderExists(id) ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet]
        public IActionResult GetOrdersForCalender()
        {
            IEnumerable<CalenderDTO> calenderDTOs = _orderRepository.GetOrdersForCalender();
            return calenderDTOs is not null ? Ok(calenderDTOs) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("{name}")]
        public async Task<IActionResult> SearchOrdersByName(string name)
        {
            var orders = await _orderRepository.SearchOrdersByName(name);
            return orders is not null ? Ok(orders) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SearchOrdersByStatus(bool status)
        {
            IEnumerable<Order> orders = await _orderRepository.GetOrders(status);
            return orders is not null ? Ok(orders) : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}