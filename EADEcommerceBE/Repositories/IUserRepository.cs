﻿/*********************************************** 
    Repository Interface of User Mgmt
    All the methods within User Mgmt
    Dilshan W.A.B. - IT21343216
 **********************************************/

using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IUserRepository
    {
        Task<ObjectId> Create(User user);
        Task<User> GetSingleUser(ObjectId objectId);
        Task<IEnumerable<User>> GetAllUsers();
        Task<bool> UpdateUser(ObjectId objectId, User user);
        Task<bool> UpdateAccountStatusById(ObjectId objectId, string accountStatus);
        Task<bool> DeleteUser(ObjectId objectId);
        Task<User?> Login(string email, string password);
    }
}
