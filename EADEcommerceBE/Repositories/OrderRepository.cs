using MongoDB.Bson;
using MongoDB.Driver;
using OrderManagement.Model;

namespace OrderManagement.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public OrderRepository(IMongoClient client)
        {
            var database = client.GetDatabase("OrderDB");
            var collection = database.GetCollection<Order>(nameof(Order));
            _orders = collection;
        }

        public async Task<ObjectId> Create(Order order)
        {
            await _orders.InsertOneAsync(order);
            return order.Id;
        }

        public async Task<bool> Delete(ObjectId objectId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, objectId);
            var result = await _orders.DeleteOneAsync(filter);
            return result.DeletedCount == 1;
        }

        public Task<Order> Get(ObjectId objectId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, objectId);
            return _orders.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Order>> GetAll()
        {
            return await _orders.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByUserId(string userId)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.UserId, userId);
            return await _orders.Find(filter).ToListAsync();
        }

        public async Task<bool> Update(ObjectId objectId, Order order)
        {
            var filter = Builders<Order>.Filter.Eq(x => x.Id, objectId);
            var update = Builders<Order>.Update
                .Set(x => x.UserId, order.UserId)
                .Set(x => x.Products, order.Products)
                .Set(x => x.TotalPrice, order.TotalPrice)
                .Set(x => x.DeliveryStatus, order.DeliveryStatus)
                .Set(x => x.OrderStatus, order.OrderStatus)
                .Set(x => x.OrderNumber, order.OrderNumber)
                .Set(x => x.IsCancel, order.IsCancel)
                .Set(x => x.CancellationNote, order.CancellationNote)
                .Set(x => x.OrderDate, order.OrderDate);

            var result = await _orders.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }

        public async Task<IEnumerable<Order>> GetByName(string name)
        {
            var filter = Builders<Order>.Filter.Eq(o => o.UserId, name);
            return await _orders.Find(filter).ToListAsync();
        }

    }
}
