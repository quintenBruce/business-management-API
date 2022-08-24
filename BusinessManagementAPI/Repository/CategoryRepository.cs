using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly OrdersContext _ordersContext;

        public CategoryRepository(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }
        public async Task<bool> CreateCategory(Category category)
        {
            _ordersContext.Categories.Add(category);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var category = _ordersContext.Categories.Find(id);
            _ordersContext.Remove(category);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<Category> GetCategory(int id)
        {
            return await _ordersContext.Categories.FindAsync(id);
        }

        public async Task<IEnumerable<Category>> GetCategories()
        {
            return await _ordersContext.Categories.ToListAsync();
        }

        public async Task<bool> UpdateCategory(Category category)
        {
            _ordersContext.Categories.Update(category);
            return await _ordersContext.SaveChangesAsync() == 1;
        }
    }
}