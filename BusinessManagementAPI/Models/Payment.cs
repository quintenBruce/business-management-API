using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessManagementAPI.Models
{
    public partial class Payment
    {
        public int Id { get; set; }
        public float Amount { get; set; }
        public string Type { get; set; } = null!;
        public int OrderId { get; set; }
        

        public virtual Order Order { get; set; } = null!;
    }
}
