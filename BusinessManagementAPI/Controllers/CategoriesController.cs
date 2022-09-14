using BusinessManagementAPI.Filters;
using BusinessManagementAPI.Models;
using BusinessManagementAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BusinessManagementAPI.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoriesController : Controller
    {
        private ICategoryRepository _categoryRepository;

        public CategoriesController(ICategoryRepository categoryRepository)
        { 
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategory(int id)
        {
            Category category = await _categoryRepository.GetCategory(id);
            return category is not null ? Ok(category) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            IEnumerable<Category> categorys = await _categoryRepository.GetCategories();
            return Utilities.IsAny(categorys) ? Ok(categorys) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCategory(Category category)
        {
            bool status = await _categoryRepository.UpdateCategory(category);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            bool status = await _categoryRepository.DeleteCategory(id);
            return status ? Ok() : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}