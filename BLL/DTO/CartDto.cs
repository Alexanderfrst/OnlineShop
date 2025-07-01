namespace BLL.DTO
{
    public record CartDto(
        int Id,
        UserDto User,
        List<CartItemDto> Items,
        decimal TotalPrice   
    );
}
