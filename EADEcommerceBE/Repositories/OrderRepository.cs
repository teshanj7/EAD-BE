/*********************************************** 
    Repository Class of Order Mgmt
    All the methods within Order Mgmt
    Gunatilleke M.B.D.S. - IT21321436
 **********************************************/

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

        //Order Create
        public string CreateOrder(Order order)
        {
            _orders.InsertOne(order);
            return order.Id;
        }

        //Delete an order using order id
        public bool DeleteOrderById(string orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var result = _orders.DeleteOne(filter);
            return result.DeletedCount == 1;
        }

        //Fetch an order by order id
        public async Task<IEnumerable<Order>> GetOrderByIdAsync(string orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var orders = await _orders.Find(filter).ToListAsync();

            // Map to a new list of orders (this part is unnecessary if you just need to return the orders)
            var orderList = orders.Select(order => new Order
            {
                Id = order.Id,
                UserId = order.UserId,
                Email = order.Email,
                Products = order.Products,
                TotalPrice = order.TotalPrice,
                DeliveryStatus = order.DeliveryStatus,
                OrderStatus = order.OrderStatus,
                OrderNumber = order.OrderNumber,
                IsCancel = order.IsCancel,
                CancellationNote = order.CancellationNote,
                OrderDate = order.OrderDate
            });

            return orderList;
        }
        
        //Fetch all orders
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            var orders = await _orders.Find(_ => true).ToListAsync();  // Asynchronously fetching the orders from MongoDB

            // Map to a new list of orders (this part is unnecessary if you just need to return the orders)
            var orderList = orders.Select(order => new Order
            {
                Id = order.Id,
                UserId = order.UserId,
                Email = order.Email,
                Products = order.Products,
                TotalPrice = order.TotalPrice,
                DeliveryStatus = order.DeliveryStatus,
                OrderStatus = order.OrderStatus,
                OrderNumber = order.OrderNumber,
                IsCancel = order.IsCancel,
                CancellationNote = order.CancellationNote,
                OrderDate = order.OrderDate
            });

            return orderList;
        }

        //Fetch orders by Customer Id
        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(string userId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.UserId, userId);
            var orders = await _orders.Find(filter).ToListAsync();

            // Map to a new list of orders (this part is unnecessary if you just need to return the orders)
            var orderList = orders.Select(order => new Order
            {
                Id = order.Id,
                UserId = order.UserId,
                Email = order.Email,
                Products = order.Products,
                TotalPrice = order.TotalPrice,
                DeliveryStatus = order.DeliveryStatus,
                OrderStatus = order.OrderStatus,
                OrderNumber = order.OrderNumber,
                IsCancel = order.IsCancel,
                CancellationNote = order.CancellationNote,
                OrderDate = order.OrderDate
            });

            return orderList;
        }

        //Update Order using order Id
        public bool UpdateOrderById(string orderId, Order order)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var updateOrder = Builders<Order>.Update
                .Set(x => x.UserId, order.UserId)
                .Set(x => x.Email, order.Email)
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

        //Update Partially Delivery Status using the Order Id
        public bool UpdatePartialDeliveryStatus(string orderId, string productVendor)
        {
            var filter = Builders<Order>.Filter.And(
                Builders<Order>.Filter.Eq(x => x.Id, orderId),
                Builders<Order>.Filter.ElemMatch(x => x.Products, p => p.ProductVendor == productVendor) // Use ProductVendor here
            );

            var updateStatus = Builders<Order>.Update.Set(x => x.OrderStatus, "Partially Delivered");

            var result = _orders.UpdateOne(filter, updateStatus);
            return result.ModifiedCount == 1;
        }

        //Update Delivery Status using the order Id
        public bool MarkOrderAsDelivered(string orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var updateStatus = Builders<Order>.Update.Set(x => x.OrderStatus, "Delivered");

            var result = _orders.UpdateOne(filter, updateStatus);
            return result.ModifiedCount == 1;
        }

        //Cancel Order using the order Id
        public bool CancelOrderById(string orderId, string cancellationNote)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            var updateCancel = Builders<Order>.Update
                .Set(x => x.IsCancel, true)
                .Set(x => x.CancellationNote, cancellationNote);

            var result = _orders.UpdateOne(filter, updateCancel);
            return result.ModifiedCount == 1;
        }

        //Get order details using the order Id
        public Order TrackOrderById(string orderId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, orderId);
            return _orders.Find(filter).FirstOrDefault();
        }

        
    }
}
