using EadECommerce.Models;
using EADEcommerceBE.Repositories;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var id = await _productRepository.CreateProduct(product);
            return new JsonResult(id.ToString());
        }

        [HttpGet("getProductById/{id}")]
        public async Task<IActionResult> GetProductById(string id)
        {
            var product = await _productRepository.GetProductById(ObjectId.Parse(id));
            return new JsonResult(product);
        }

        [HttpGet("getProductByName/{name}")]
        public async Task<IActionResult> GetProductByName(string name)
        {
            var product = await _productRepository.GetProductByName(name);
            return new JsonResult(product);
        }

        [HttpGet("getAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productRepository.GetAllProducts();
            return new JsonResult(products);
        }

        [HttpDelete("deleteProductById/{id}")]
        public async Task<IActionResult> DeleteProduct(string id)
        {
            var product = await _productRepository.DeleteProductById(ObjectId.Parse(id));
            return new JsonResult(product);
        }

        [HttpPut("updateProductById/{id}")]
        public async Task<IActionResult> UpdateProductById(string id, Product Product)
        {
            var product = await _productRepository.UpdateProductById(ObjectId.Parse(id), Product);
            return new JsonResult(product);
        }

        [HttpPut("updateProductStatus/{id}")]
        public async Task<IActionResult> UpdateProductStatus(string id, Product Product)
        {
            var product = await _productRepository.UpdateProductStatusById(ObjectId.Parse(id), Product);
            return new JsonResult(product);
        }

        [HttpPut("updateProductAvailability/{id}")]
        public async Task<IActionResult> UpdateProductAvailability(string id, Product Product)
        {
            var product = await _productRepository.UpdateProductAvailability(ObjectId.Parse(id), Product);
            return new JsonResult(product);
        }



    }
}
