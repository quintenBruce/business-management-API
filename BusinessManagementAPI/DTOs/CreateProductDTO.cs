using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class CreateProductDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public Category? Category { get; set; }
        public float Price { get; set; }
        public string? Dimensions { get; set; }
        public int OrderId { get; set; }
        public int CategoryId { get; set; }
        public static Product ToProduct(CreateProductDTO createProductDTO)
        {
            return new Product
            {
                Id = createProductDTO.Id,
                Name = createProductDTO.Name,
                Description = createProductDTO.Description,
                Category = createProductDTO.Category,
                Price = createProductDTO.Price,
                Dimensions = createProductDTO.Dimensions,
                OrderId = createProductDTO.OrderId,
                CategoryId = createProductDTO.Category.Id
            };

        }
    }
}
