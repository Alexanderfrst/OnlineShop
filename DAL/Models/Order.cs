using System;
using System.Collections.Generic;

namespace DAL.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}