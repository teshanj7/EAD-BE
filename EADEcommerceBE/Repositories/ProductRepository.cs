﻿/*********************************************** 
    Repository Class of Product Mgmt
    All the methods within Product Mgmt
    Jayakody T.N.A. - IT21345296
 **********************************************/

using EADEcommerceBE.Models;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EADEcommerceBE.Repositories
{
    public class ProductRepository : IProductRepository
    {
        public readonly IMongoCollection<Product> _products;
        public ProductRepository(IMongoClient client)
        {
            var database = client.GetDatabase("ProductDB");
            var collection = database.GetCollection<Product>(nameof(Product));

            _products = collection;
        }

        //Create Product
        public async Task<ObjectId> CreateProduct(Product product)
        {
            await _products.InsertOneAsync(product);
            return product.Id;
        }

        //Delete product using Product Id
        public async Task<bool> DeleteProductById(ObjectId productId)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, productId);
            var result = await _products.DeleteOneAsync(filter);

            return result.DeletedCount == 1;
        }

        //Fetch all products
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            var products = await _products.Find(_ => true).ToListAsync();

            var productList = products.Select(product => new Product
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductCategory = product.ProductCategory,
                ProductDescription = product.ProductDescription,
                ProductQuantity = product.ProductQuantity,
                ProductVendor = product.ProductVendor,
                ProductStatus = product.ProductStatus,
                ProductAvailability = product.ProductAvailability,
                ProductImage = product.ProductImage,
                ProductPrice = product.ProductPrice,
            });

            return productList;
        }

        //Return Products using Vendor Id
        public async Task<IEnumerable<Product>> GetProductsByVendor(string vendor)
        {
            var products = await _products.Find(product => product.ProductVendor == vendor).ToListAsync();

            var productList = products.Select(product => new Product
            {
                Id = product.Id,
                ProductName = product.ProductName,
                ProductCategory = product.ProductCategory,
                ProductDescription = product.ProductDescription,
                ProductQuantity = product.ProductQuantity,
                ProductVendor = product.ProductVendor,
                ProductStatus = product.ProductStatus,
                ProductAvailability = product.ProductAvailability,
                ProductImage = product.ProductImage,
                ProductPrice = product.ProductPrice,
            });

            return productList;
        }

        //Return Product By Product Id
        public Task<Product> GetProductById(ObjectId productId)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, productId);
            var product = _products.Find(filter).FirstOrDefaultAsync();

            return product;

        }

        //Return Product By Product Name
        public async Task<IEnumerable<Product>> GetProductByName(string Name)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.ProductName, Name);
            var product = await _products.Find(filter).ToListAsync();

            return product;
        }

        //Update Product Availability using product Id
        public async Task<bool> UpdateProductAvailability(ObjectId productId, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, productId);
            var updateAvailability = Builders<Product>.Update
                .Set(x => x.ProductAvailability, product.ProductAvailability);

            var result = await _products.UpdateOneAsync(filter, updateAvailability);
            return result.ModifiedCount == 1;
        }

        //Update Product using product Id
        public async Task<bool> UpdateProductById(object id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var updateProduct = Builders<Product>.Update
                .Set(x => x.ProductName, product.ProductName)
                .Set(x => x.ProductCategory, product.ProductCategory)
                .Set(x => x.ProductDescription, product.ProductDescription)
                .Set(x => x.ProductQuantity, product.ProductQuantity)
                .Set(x => x.ProductVendor, product.ProductVendor)
                .Set(x => x.ProductStatus, product.ProductStatus)
                .Set(x => x.ProductAvailability, product.ProductAvailability)
                .Set(x => x.ProductImage, product.ProductImage)
                .Set(x => x.ProductPrice, product.ProductPrice);

            var result = await _products.UpdateOneAsync(filter, updateProduct);
            return result.ModifiedCount == 1;
        }

        //Update Product Status using product Id
        public async Task<bool> UpdateProductStatusById(object id, Product product)
        {
            var filter = Builders<Product>.Filter.Eq(x => x.Id, id);
            var updateStatus = Builders<Product>.Update
                .Set(x => x.ProductStatus, product.ProductStatus);

            var result = await _products.UpdateOneAsync(filter, updateStatus);
            return result.ModifiedCount == 1;
        }
    }
}
