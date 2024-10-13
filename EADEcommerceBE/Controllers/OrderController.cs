/*********************************************** 
    Order Controller
    All API end points of Order Management
    Gunatilleke M.B.D.S. - IT21321436
 **********************************************/

using EADEcommerceBE.Models;
using EADEcommerceBE.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace EADEcommerceBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        // Create a new order
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            var id = await _orderRepository.CreateOrder(order);
            return new JsonResult(order);
        }

        // Get order by ID
        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetOrderById(string id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            return new JsonResult(order);
        }

        // Track order by ID
        [HttpGet("track/{id}")]
        public IActionResult TrackOrderById(string id)
        {
            var order = _orderRepository.TrackOrderById(id);
            return new JsonResult(order);
        }

        // Get all orders
        [HttpGet("getAllOrders")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderRepository.GetAllOrders();
            return new JsonResult(orders);
        }

        // Get orders by user ID
        [HttpGet("getOrdersByUser/{userId}")]
        public async Task<IActionResult> GetOrdersByUserId(string userId)
        {
            var orders = await _orderRepository.GetOrdersByUserIdAsync(userId);
            return new JsonResult(orders);
        }

        // Update an order by ID
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, Order order)
        {
            var result = _orderRepository.UpdateOrderById(id, order);
            return new JsonResult(result);
        }

        // Delete an order by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(string id)
        {
            var result = _orderRepository.DeleteOrderById(id);
            return new JsonResult(result);
        }

        // Cancel an order with a cancellation note
        [HttpPut("cancel/{id}")]
        public IActionResult CancelOrder(string id, [FromBody] string cancellationNote)
        {
            var result = _orderRepository.CancelOrderById(id, cancellationNote);
            return new JsonResult(result);
        }

        // Mark an order as delivered
        [HttpPut("markAsDelivered/{id}")]
        public IActionResult MarkOrderAsDelivered(string id)
        {
            var result = _orderRepository.MarkOrderAsDelivered(id);
            return new JsonResult(result);
        }

        // Update the delivery status for a vendor's partial delivery
        [HttpPut("partialDelivery/{id}/{vendorId}")]
        public IActionResult UpdatePartialDelivery(string id, string vendorId)
        {
            var result = _orderRepository.UpdatePartialDeliveryStatus(id, vendorId);
            return new JsonResult(result);
        }
    }
}
