using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessManagementAPI.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public float PaymentAmount { get; set; }
        public string PaymentType { get; set; } = null!;
        public int OrderId { get; set; }
        [JsonIgnore]

        public virtual Order Order { get; set; } = null!;
    }
}
