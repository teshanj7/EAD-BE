﻿using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IUserRepository
    {
        Task<ObjectId> Create(User user);
        Task<User> GetSingleUser(ObjectId objectId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> UpdateUser(ObjectId objectId, User user);
        Task<bool> DeleteUser(ObjectId objectId);
    }
}
