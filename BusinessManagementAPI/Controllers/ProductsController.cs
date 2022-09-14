using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Filters;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetProduct(int id)
        {
            IEnumerable<Product> product = await _productRepository.GetProducts(id);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            IEnumerable<Product> products = await _productRepository.GetProducts(id);
            return Utilities.IsAny(products) ? Ok(products) : NotFound();
        }

        [HttpPost("{orderId}")]
        public async Task<IActionResult> UpdateProducts(List<ProductDTO> productDTOs, int orderId)
        {
            IEnumerable<ProductDTO> updatedProducts = await _productRepository.UpdateProducts(productDTOs, orderId);
            return updatedProducts is not null ? Ok(updatedProducts) : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool status = await _productRepository.DeleteProduct(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProducts(List<CreateProductDTO> products)
        {
            return await _productRepository.CreateProducts(products) ? Ok() : NotFound();
        }
    }
}