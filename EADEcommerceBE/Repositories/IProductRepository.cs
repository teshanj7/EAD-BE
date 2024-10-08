﻿/*********************************************** 
    Repository Interface of Product Mgmt
    All the methods within Product Mgmt
    Jayakody T.N.A. - IT21345296
 **********************************************/

using EADEcommerceBE.Models;
using MongoDB.Bson;

namespace EADEcommerceBE.Repositories
{
    public interface IProductRepository
    {
        Task<ObjectId> CreateProduct(Product product);
        Task<Product> GetProductById(ObjectId productId);
        Task<IEnumerable<Product>> GetAllProducts();
        Task<IEnumerable<Product>> GetProductsByVendor(string vendor);
        Task<IEnumerable<Product>> GetProductByName(string Name);
        Task<bool> UpdateProductById(Object id, Product product);
        Task<bool> DeleteProductById(ObjectId productId);
        Task<bool> UpdateProductStatusById(Object id, Product product);
        Task<bool> UpdateProductAvailability(ObjectId productId, Product product);


    }
}
