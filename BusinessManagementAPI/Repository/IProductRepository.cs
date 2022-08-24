using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<Product> GetProduct(int id);
        Task<bool> DeleteProduct(int id);
        Task<bool> UpdateProduct(Product product);
        Task<bool> CreateProduct(Product product);
    }
}
