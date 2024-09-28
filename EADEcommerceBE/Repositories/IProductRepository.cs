
using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IProductRepository
    {
        Task<ObjectId> CreateProduct(Product product);
        Task<Product> GetProductById(ObjectId productId);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductByName(string Name);
        Task<bool> UpdateProductById(Object id, Product product);
        Task<bool> DeleteProductById(ObjectId productId);
        Task<bool> UpdateProductStatusById(Object id, Product product);
        Task<bool> UpdateProductAvailability(ObjectId productId, Product product);


    }
}
