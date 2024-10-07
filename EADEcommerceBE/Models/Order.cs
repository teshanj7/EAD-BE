/*********************************************** 
    Order Class
    Attributes of the Order Class
    Gunatilleke M.B.D.S. - IT21321436
 **********************************************/

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EADEcommerceBE.Models
{
    public class Order
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        public string UserId { get; set; }

        public string Email { get; set; }

        public List<Product> Products { get; set; }

        public double TotalPrice { get; set; }
        
        public string DeliveryStatus { get; set; }  // Processing, Shipped, Delivered, etc.
        
        public string OrderStatus { get; set; }  // Approve or Cancel
        
        public string OrderNumber { get; set; }
        
        public bool IsCancel { get; set; }  // If true, provide a cancellation note
        
        public string CancellationNote { get; set; }  // Reason for cancellation
        
        public DateTime OrderDate { get; set; }
    }

}
