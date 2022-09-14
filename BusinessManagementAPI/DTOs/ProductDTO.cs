using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }

        public float Price { get; set; }
        public string? Dimensions { get; set; }
        public virtual Category? Category { get; set; }

        public static Product ToProducts(ProductDTO productDTO)
        {
            return new Product
            {
                Id = productDTO.Id,
                Category = productDTO.Category,
                Dimensions = productDTO.Dimensions,
                Price = productDTO.Price,
                CategoryId = productDTO.CategoryId,
                Description = productDTO.Description,
                Name = productDTO.Name
            };
        }
    }
}