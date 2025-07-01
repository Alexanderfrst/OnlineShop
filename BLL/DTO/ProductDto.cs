namespace BLL.DTO
{
    public record ProductDto(
        int Id,
        string Name,
        string Description,
        decimal Price,
        int StockQuantity,
        CategoryDto Category
    );
}