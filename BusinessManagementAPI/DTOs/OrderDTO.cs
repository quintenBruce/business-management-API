﻿using BusinessManagementAPI.Models;

namespace BusinessManagementAPI.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public float Total { get; set; }
        public DateTime PlacementDate { get; set; }
        public DateTime FulfillmentDate { get; set; }
        public string ComThread { get; set; } = null!;
        public bool Status { get; set; }
        public int DeliveryFee { get; set; }
        public bool? OutOfTown { get; set; }
        public float Balance { get; set; }
        public DateTime? CompletionDate { get; set; }
        public CustomerDTO Customer { get; set; }
        public ICollection<ProductDTO>? Products { get; set; }
        public ICollection<PaymentDTO>? Payments { get; set; }

        public static Order ToOrder(OrderDTO orderDTO)
        {
            return new Order
            {
                Id = orderDTO.Id,
                CustomerId = orderDTO.Customer is not null ? orderDTO.Customer.Id : 0,
                Total = orderDTO.Total,
                PlacementDate = orderDTO.PlacementDate,
                FulfillmentDate = orderDTO.FulfillmentDate,
                Status = orderDTO.Status,
                DeliveryFee = orderDTO.DeliveryFee,
                OutOfTown = orderDTO.OutOfTown,
                ComThread = orderDTO.ComThread,
                Balance = orderDTO.Balance,
                CompletionDate = orderDTO.CompletionDate,
                Customer = CustomerDTO.ToCustomer(orderDTO.Customer),
                Products = orderDTO.Products is not null ? orderDTO.Products.Select(x => ProductDTO.ToProducts(x)).ToList() : new List<Product> { },
                Payments = orderDTO.Payments is not null ? orderDTO.Payments.Select(x => PaymentDTO.ToPayment(x)).ToList() : new List<Payment> { },
            };
        }

        public static Customer ToCustomer(OrderDTO orderDTO)
        {
            return CustomerDTO.ToCustomer(orderDTO.Customer);
        }
    }
}