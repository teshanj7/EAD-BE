/*********************************************** 
    Rating Class
    Attributes of the Rating Class
    Dilshan W.A.B. - IT21343216
 **********************************************/

using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace EADEcommerceBE.Models
{
    public class Rating
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        public required string Name { get; set; }
        public string? CusId { get; set; }
        public string? VendorId { get; set; } 
        public string? Comment { get; set; } 
        public int RatingNo { get; set; } 
    }
}
