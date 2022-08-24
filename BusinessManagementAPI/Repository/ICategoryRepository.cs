using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetCategories();
        Task<Category> GetCategory(int id);
        Task<bool> DeleteCategory(int id);
        Task<bool> UpdateCategory(Category category);
        Task<bool> CreateCategory(Category category);
    }
}
