using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessManagementAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersContext _ordersContext;

        public ProductRepository(OrdersContext ordersContext)
        {
            _ordersContext = ordersContext;
        }

        public async Task<bool> CreateProduct(Product product)
        {
            _ordersContext.Products.Add(product);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = _ordersContext.Products.Find(id);
            _ordersContext.Remove(product);
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _ordersContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _ordersContext.Products.Include(x => x.Order).ToListAsync();
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            _ordersContext.Products.Update(product);
            return await _ordersContext.SaveChangesAsync() == 1;
        }
    }
}