using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessManagementAPI.Models
{
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public int? CategoryId { get; set; }
        public int OrderId { get; set; }
        public float Price { get; set; }
        public string? Dimensions { get; set; }

        public virtual Category? Category { get; set; }
        [JsonIgnore]
        public virtual Order Order { get; set; } = null!;
    }
}
