/*********************************************** 
    Repository Class of User Mgmt
    All the methods within User Mgmt
    Dilshan W.A.B. - IT21343216
 **********************************************/

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

        //Create new user
        public async Task<ObjectId> Create(User user)
        {
            await _users.InsertOneAsync(user);
            return user.Id;
        }

        //Delete User using user Id
        public async Task<bool> DeleteUser(ObjectId objectId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, objectId);
            var result = await _users.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        //Fetch all users
        public async Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await _users.Find(_ => true).ToListAsync();
            // Map UserId (ObjectId) to string for each user
            var userList = users.Select(user => new User
            {
                Id = user.Id,  // No need to change, it will be handled later in the controller
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserType = user.UserType,
                IsWebUser = user.IsWebUser,
                Username = user.Username,
                AccountStatus = user.AccountStatus,
                Password = user.Password,
                AvgRating = user.AvgRating
            });

            return userList;
        }

        //Return User using user id
        public Task<User> GetSingleUser(ObjectId objectId)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, objectId);
            var user = _users.Find(filter).FirstOrDefaultAsync();

            return user;
        }
        // Login API
        public async Task<User?> Login(string email, string password)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Email, email) & Builders<User>.Filter.Eq(x => x.Password, password);
            var user = await _users.Find(filter).FirstOrDefaultAsync();
            // Check if the user exists and their account status is "Active"
            if (user == null)
            {
                return null; // Return null if account is not active
            }
            return user;
        }

        //Update account status using user id
        public async Task<bool> UpdateAccountStatusById(ObjectId objectId, string accountStatus)
        {
            var filter = Builders<User>.Filter.Eq(x => x.Id, objectId);
            var updateAccountStatus = Builders<User>.Update.Set(x => x.AccountStatus, accountStatus);

            var result = await _users.UpdateOneAsync(filter, updateAccountStatus);
            return result.ModifiedCount == 1;
        }

        //Update User using user id
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
