using EADEcommerceBE.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EADEcommerceBE.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IMongoClient client)
        {
            var database = client.GetDatabase("UserDB");
            var collection = database.GetCollection<User>(nameof(User));

            _users = collection;
        }
        public async Task<ObjectId> Create(User user)
        {
            await _users.InsertOneAsync(user);
            return user.Id;
        }
        public async Task<bool> DeleteUser(ObjectId objectId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, objectId);
            var result = await _users.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _users.Find(_ => true).ToListAsync();
            return users;
        }
        public Task<User> GetSingleUser(ObjectId objectId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, objectId);
            var user = _users.Find(filter).FirstOrDefaultAsync();

            return user;
        }
        public async Task<bool> UpdateUser(ObjectId objectId, User user)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, objectId);
            var update = Builders<User>.Update
                .Set(x => x.Name, user.Name)
                .Set(x => x.Email, user.Email)
                .Set(x => x.Address, user.Address)
                .Set(x => x.Phone, user.Phone)
                .Set(x => x.Username, user.Username)
                .Set(x => x.AccountStatus, user.AccountStatus)
                .Set(x => x.Password, user.Password)
                .Set(x => x.AvgRating, user.AvgRating);

            var result = await _users.UpdateOneAsync(filter, update);
            return result.ModifiedCount == 1;
        }
    }
}
