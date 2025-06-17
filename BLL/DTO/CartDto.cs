using System.Collections.Generic;

namespace BLL.DTO
{
    public class CartDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public List<CartItemDto> Items { get; set; }
    }
}
