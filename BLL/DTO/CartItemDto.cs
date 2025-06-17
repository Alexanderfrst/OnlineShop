namespace BLL.DTO
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public ProductDto Product { get; set; }
        public int Quantity { get; set; }
    }
}