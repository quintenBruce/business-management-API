using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class OrderGroupDTO
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public float Total { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime OrderFulfillmentDate { get; set; }
        public string ComThread { get; set; } = null!;
        public bool OrderStatus { get; set; }
        public int DeliveryFee { get; set; }
        public bool? OutOfTown { get; set; }
        public float Balance { get; set; }
        public DateTime? OrderCompletionDate { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<PaymentDTO> Payments { get; set; }
        public virtual ICollection<ProductDTO> Products { get; set; }

        public static Order ToOrder(OrderGroupDTO groupDTO)
        {
            return new Order
            {
                Id = groupDTO.Id,
                CustomerId = groupDTO.CustomerId,
                Total = groupDTO.Total,
                PlacementDate = groupDTO.OrderDate,
                FulfillmentDate = groupDTO.OrderFulfillmentDate,
                Status = groupDTO.OrderStatus,
                DeliveryFee = groupDTO.DeliveryFee,
                OutOfTown = groupDTO.OutOfTown,
                ComThread = groupDTO.ComThread,
                Balance = groupDTO.Balance,
                CompletionDate = groupDTO.OrderCompletionDate,
                Customer = groupDTO.Customer,
                Payments = groupDTO.Payments.Select(x => new Payment { Id = x.Id, Amount = x.Amount, Type = x.Type }).ToList(),
                Products = groupDTO.Products.Select(x => new Product { Price = x.Price, Category = x.Category, Description = x.Description, Dimensions = x.Dimensions, Id = x.Id, Name = x.Name, CategoryId = x.CategoryId }).ToList()
            };
        }
    }
}