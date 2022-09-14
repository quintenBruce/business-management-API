using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts(int id);

        Task<bool> DeleteProduct(int id);

        Task<IEnumerable<ProductDTO>> UpdateProducts(List<ProductDTO> products, int orderId);

        Task<bool> CreateProducts(List<CreateProductDTO> products);
    }
}