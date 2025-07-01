namespace BLL.DTO
{
    public record OrderDto(
        int Id,
        UserDto User,
        DateTime CreatedAt,
        string Status,
        decimal TotalPrice,
        string DeliveryMethod,
        List<OrderItemDto> Items
    );
}
