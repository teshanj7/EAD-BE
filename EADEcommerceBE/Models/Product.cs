using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EADEcommerceBE.Models
{
    public class Product
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]

        public ObjectId Id { get; set; }
        public string? ProductName { get; set; }
        public string? ProductCategory { get; set; }
        public string? ProductDescription { get; set; }
        public int ProductQuantity { get; set; }
        public string? ProductVendor { get; set; }
        public bool ProductStatus { get; set; }
        public bool ProductAvailability { get; set; }
        public string? ProductImage { get; set; }
        public int ProductPrice { get; set; }
    }
}
