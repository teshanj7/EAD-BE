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
        public IActionResult Create(Order order)
        {
            var id = _orderRepository.CreateOrder(order);
            return new JsonResult(id.ToString());
        }

        // Get order by ID
        [HttpGet("get/{id}")]
        public IActionResult GetOrderById(string id)
        {
            var order = _orderRepository.GetOrderById(ObjectId.Parse(id));
            return new JsonResult(order);
        }

        // Track order by ID
        [HttpGet("track/{id}")]
        public IActionResult TrackOrderById(string id)
        {
            var order = _orderRepository.TrackOrderById(ObjectId.Parse(id));
            return new JsonResult(order);
        }

        // Get all orders
        [HttpGet("getAllOrders")]
        public IActionResult GetAllOrders()
        {
            var orders = _orderRepository.GetAllOrders();
            return new JsonResult(orders);
        }

        // Get orders by user ID
        [HttpGet("getOrdersByUser/{userId}")]
        public IActionResult GetOrdersByUserId(string userId)
        {
            var orders = _orderRepository.GetOrdersByUserId(userId);
            return new JsonResult(orders);
        }

        // Update an order by ID
        [HttpPut("{id}")]
        public IActionResult UpdateOrder(string id, Order order)
        {
            var result = _orderRepository.UpdateOrderById(ObjectId.Parse(id), order);
            return new JsonResult(result);
        }

        // Delete an order by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteOrder(string id)
        {
            var result = _orderRepository.DeleteOrderById(ObjectId.Parse(id));
            return new JsonResult(result);
        }

        // Cancel an order with a cancellation note
        [HttpPut("cancel/{id}")]
        public IActionResult CancelOrder(string id, [FromBody] string cancellationNote)
        {
            var result = _orderRepository.CancelOrderById(ObjectId.Parse(id), cancellationNote);
            return new JsonResult(result);
        }

        // Mark an order as delivered
        [HttpPut("markAsDelivered/{id}")]
        public IActionResult MarkOrderAsDelivered(string id)
        {
            var result = _orderRepository.MarkOrderAsDelivered(ObjectId.Parse(id));
            return new JsonResult(result);
        }

        // Update the delivery status for a vendor's partial delivery
        [HttpPut("partialDelivery/{id}/{vendorId}")]
        public IActionResult UpdatePartialDelivery(string id, string vendorId)
        {
            var result = _orderRepository.UpdatePartialDeliveryStatus(ObjectId.Parse(id), vendorId);
            return new JsonResult(result);
        }
    }
}
