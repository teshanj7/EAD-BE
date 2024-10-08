/*********************************************** 
    Repository Interface of Order Mgmt
    All the methods within Order Mgmt
    Gunatilleke M.B.D.S. - IT21321436
 **********************************************/

using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrder(Order order);
        bool DeleteOrderById(string orderId);
        Task<IEnumerable<Order>> GetOrderByIdAsync(string orderId);
        Task<IEnumerable<Order>> GetAllOrders();
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId);
        bool UpdateOrderById(string orderId, Order order);
        bool UpdatePartialDeliveryStatus(string orderId, string productVendor);
        bool MarkOrderAsDelivered(string orderId);
        bool CancelOrderById(string orderId, string cancellationNote);
        Order TrackOrderById(string orderId);
    }
}
