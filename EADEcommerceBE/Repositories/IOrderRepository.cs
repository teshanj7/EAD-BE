using EADEcommerceBE.Models;
using MongoDB.Bson;


namespace EADEcommerceBE.Repositories
{
    public interface IOrderRepository
    {
        Task<ObjectId> Create(Order order);
        Task<Order> Get(ObjectId objectId);
        Task<IEnumerable<Order>> GetAll();
        Task<IEnumerable<Order>> GetByUserId(string userId);
        Task<IEnumerable<Order>> GetByName(string name);
        Task<bool> Update(ObjectId objectId, Order order);
        Task<bool> Delete(ObjectId objectId);
    }
}

