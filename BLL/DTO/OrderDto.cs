using System;
using System.Collections.Generic;

namespace BLL.DTO
{
    public class OrderDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }
        public List<OrderItemDto> Items { get; set; }
    }
}