namespace BLL.DTO
{
    public record OrderItemDto(int Id, ProductDto Product, int Quantity, decimal UnitPrice);
}