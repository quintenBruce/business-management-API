using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BusinessManagementAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Id = 0;
            FullName = null;
            Orders = null;
            PhoneNumber = null;
        }


        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public int? PhoneNumber { get; set; }
        [JsonIgnore]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
