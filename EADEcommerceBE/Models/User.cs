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
        public string Address { get; set; }
        public String Phone { get; set; }
        public required string UserType { get; set; }
        public required bool IsWebUser { get; set; }
        public required string Username { get; set; }
        public required string AccountStatus { get; set; }
        public required string Password { get; set; }
        public double AvgRating { get; set; }
    }
}
