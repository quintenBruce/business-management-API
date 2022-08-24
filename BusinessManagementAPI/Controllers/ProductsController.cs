using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
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
            Product product = await _productRepository.GetProduct(id);
            return product is not null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            IEnumerable<Product> products = await _productRepository.GetProducts();
            return Utilities.IsAny(products) ? Ok(products) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateProduct(Product order)
        {
            bool status = await _productRepository.UpdateProduct(order);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            bool status = await _productRepository.DeleteProduct(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}