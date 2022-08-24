using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessManagementAPI.Models
{
    public partial class Order
    {
        public Order()
        {
            Payments = new HashSet<Payment>();
            Products = new HashSet<Product>();
        }

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
        
        public virtual Customer Customer { get; set; } = null!;
        [JsonIgnore]
        public virtual ICollection<Payment> Payments { get; set; }
        [JsonIgnore]
        public virtual ICollection<Product> Products { get; set; }
    }
}
