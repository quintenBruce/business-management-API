using BusinessManagementAPI.DTOs;
using BusinessManagementAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace BusinessManagementAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly OrdersContext _ordersContext;
        private readonly IOrderRepository _orderRepository;

        public ProductRepository(OrdersContext ordersContext, IOrderRepository orderRepository)
        {
            _ordersContext = ordersContext;
            _orderRepository = orderRepository;
        }

        public async Task<bool> CreateProducts(List<CreateProductDTO> products)
        {
            var productsList = products.Select(x => CreateProductDTO.ToProduct(x)).ToList();
            var order = _ordersContext.Orders.Where(x => x.Id == productsList.ElementAt(0).OrderId).First();
            foreach (var product in productsList)
            {
                product.CategoryId = _ordersContext.Categories.AsNoTracking().First(x => x.Name.Trim() == product.Category.Name.Trim()).Id;
                product.Category = null;

                
                order.Balance += product.Price;
                order.Total += product.Price;
                
            }

            _ordersContext.Products.AddRange(productsList);

 

            return await _ordersContext.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteProduct(int id)
        {
            var product = _ordersContext.Products.Find(id);
            _ordersContext.Remove(product);
            var order = _ordersContext.Orders.First(x => x.Id == product.OrderId);
            order.Balance -= product!.Price;
            order.Total -= product.Price;
            return await _ordersContext.SaveChangesAsync() == 1;
        }

        

        public async Task<IEnumerable<Product>> GetProducts(int id)
        {
            return await _ordersContext.Products
                .Include(x => x.Category)
                .Include(x => x.Order)
                .Where(p => (id > 0) ? p.OrderId == id : true)
                .Select(x => new Product
                {
                    OrderId = x.OrderId,
                    Id = x.Id,
                    Category = x.Category,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Dimensions = x.Dimensions,
                })
                .ToListAsync();
        }

        

        public async Task<IEnumerable<ProductDTO>> UpdateProducts(List<ProductDTO> products, int orderId)
        {
            List<Product> productList = (List<Product>)products.Select(x => ProductDTO.ToProducts(x)).ToList();
            
            productList.ForEach(q =>
            {
                q.CategoryId = _ordersContext.Categories.AsNoTracking().First(x => x.Name.Trim() == q.Category.Name.Trim()).Id;
                q.Category = null;
            });
                _ordersContext.Products.UpdateRange(productList);
            productList.ForEach(q =>
            {
                _ordersContext.Entry(q).Property(x => x.OrderId).IsModified = false;
                
            });
            int status = await _ordersContext.SaveChangesAsync();
            _ordersContext.ChangeTracker.Clear();

            if (status > 0)
            {
                await _orderRepository.UpdateOrderPriceAndBalance(orderId);
                return products;

            }

                
            else
                return new List<ProductDTO> { };
        }
    }
}