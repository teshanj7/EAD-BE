/*********************************************** 
    User Class
    Attributes of the User Class
    Dilshan W.A.B. - IT21343216
 **********************************************/

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EADEcommerceBE.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public required string UserType { get; set; }
        public required bool IsWebUser { get; set; }
        public required string Username { get; set; }
        public required string AccountStatus { get; set; }
        public required string Password { get; set; }
        public double AvgRating { get; set; }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }

}
