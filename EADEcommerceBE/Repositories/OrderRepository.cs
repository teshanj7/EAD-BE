using EADEcommerceBE.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EADEcommerceBE.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoClient client)
        {
            var database = client.GetDatabase("OrderDB");
            _orders = database.GetCollection<Order>(nameof(Order));
        }

        public ObjectId CreateOrder(Order order)
        {
            _orders.InsertOne(order);
            return order.Id;
        }

        public bool DeleteOrderById(ObjectId orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var result = _orders.DeleteOne(filter);
            return result.DeletedCount == 1;
        }

        public Order GetOrderById(ObjectId orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            return _orders.Find(filter).FirstOrDefault();
        }

        public IEnumerable<Order> GetAllOrders()
        {
            return _orders.Find(_ => true).ToList();
        }

        public IEnumerable<Order> GetOrdersByUserId(string userId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.UserId, userId);
            return _orders.Find(filter).ToList();
        }

        public bool UpdateOrderById(ObjectId orderId, Order order)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var updateOrder = Builders<Order>.Update
                .Set(x => x.UserId, order.UserId)
                .Set(x => x.Products, order.Products)
                .Set(x => x.TotalPrice, order.TotalPrice)
                .Set(x => x.DeliveryStatus, order.DeliveryStatus)
                .Set(x => x.OrderStatus, order.OrderStatus)
                .Set(x => x.OrderNumber, order.OrderNumber)
                .Set(x => x.IsCancel, order.IsCancel)
                .Set(x => x.CancellationNote, order.CancellationNote)
                .Set(x => x.OrderDate, order.OrderDate);

            var result = _orders.UpdateOne(filter, updateOrder);
            return result.ModifiedCount == 1;
        }

        public bool UpdatePartialDeliveryStatus(ObjectId orderId, string productVendor)
        {
            var filter = Builders<Order>.Filter.And(
                Builders<Order>.Filter.Eq(x => x.Id, orderId),
                Builders<Order>.Filter.ElemMatch(x => x.Products, p => p.ProductVendor == productVendor) // Use ProductVendor here
            );

            var updateStatus = Builders<Order>.Update.Set(x => x.OrderStatus, "Partially Delivered");

            var result = _orders.UpdateOne(filter, updateStatus);
            return result.ModifiedCount == 1;
        }


        public bool MarkOrderAsDelivered(ObjectId orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var updateStatus = Builders<Order>.Update.Set(x => x.OrderStatus, "Delivered");

            var result = _orders.UpdateOne(filter, updateStatus);
            return result.ModifiedCount == 1;
        }

        public bool CancelOrderById(ObjectId orderId, string cancellationNote)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var updateCancel = Builders<Order>.Update
                .Set(x => x.IsCancel, true)
                .Set(x => x.CancellationNote, cancellationNote);

            var result = _orders.UpdateOne(filter, updateCancel);
            return result.ModifiedCount == 1;
        }

        public Order TrackOrderById(ObjectId orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            return _orders.Find(filter).FirstOrDefault();
        }
    }
}
