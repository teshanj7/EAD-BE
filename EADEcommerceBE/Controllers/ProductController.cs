/*********************************************** 
    Product Controller
    All API end points of Product Management
    Jayakody T.N.A. - IT21345296
 **********************************************/

using EADEcommerceBE.Models;
using EADEcommerceBE.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace EADEcommerceBE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        //Create Product
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var id = await _productRepository.CreateProduct(product);
            return new JsonResult(id.ToString());
        }

        //Return Product By Product Id
        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(ObjectId.Parse(id));
            return new JsonResult(product);
        }

        //Return Product By Product Name
        [HttpGet("getProductByName/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            return new JsonResult(product);
        }

        //Fetch all products
        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();

            var productList = products.Select(product => new
            {
                productId = product.Id.ToString(),
                product.ProductName,
                product.ProductCategory,
                product.ProductDescription,
                product.ProductQuantity,
                product.ProductVendor,
                product.ProductStatus,
                product.ProductAvailability,
                product.ProductPrice
            });

            return new JsonResult(productList);
        }

        //Return Products using Vendor Id
        [HttpGet("getProductsByVendor/{vendor}")]
        public async Task<IActionResult> GetProductsByVendor(string vendor)
        {
            var products = await _productRepository.GetProductsByVendor(vendor);

            var productList = products.Select(product => new
            {
                productId = product.Id.ToString(),
                product.ProductName,
                product.ProductCategory,
                product.ProductDescription,
                product.ProductQuantity,
                product.ProductVendor,
                product.ProductStatus,
                product.ProductAvailability,
                product.ProductPrice
            });

            return new JsonResult(productList);
        }

        //Delete product using Product Id
        [HttpDelete("deleteProductById/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productRepository.DeleteProductById(ObjectId.Parse(id));
            return new JsonResult(product);
        }

        //Update Product using product Id
        [HttpPut("updateProductById/{id}")]
        public async Task<IActionResult> UpdateProductById(string id, Product Product)
        {
            var product = await _productRepository.UpdateProductById(ObjectId.Parse(id), Product);
            return new JsonResult(product);
        }

        //Update Product Status using product Id
        [HttpPut("updateProductStatus/{id}")]
        public async Task<IActionResult> UpdateProductStatus(string id, Product Product)
        {
            var product = await _productRepository.UpdateProductStatusById(ObjectId.Parse(id), Product);
            return new JsonResult(product);
        }

        //Update Product Availability using product Id
        [HttpPut("updateProductAvailability/{id}")]
        public async Task<IActionResult> UpdateProductAvailability(string id, Product Product)
        {
            var product = await _productRepository.UpdateProductAvailability(ObjectId.Parse(id), Product);
            return new JsonResult(product);
        }



    }
}
