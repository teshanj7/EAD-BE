using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IOrderRepository
    {
        ObjectId CreateOrder(Order order);
        bool DeleteOrderById(ObjectId orderId);
        Order GetOrderById(ObjectId orderId);
        IEnumerable<Order> GetAllOrders();
        IEnumerable<Order> GetOrdersByUserId(string userId);
        bool UpdateOrderById(ObjectId orderId, Order order);
        bool UpdatePartialDeliveryStatus(ObjectId orderId, string productVendor);
        bool MarkOrderAsDelivered(ObjectId orderId);
        bool CancelOrderById(ObjectId orderId, string cancellationNote);
        Order TrackOrderById(ObjectId orderId);
    }
}
