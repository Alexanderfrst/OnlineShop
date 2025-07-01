namespace BLL.DTO
{
    public record ReviewDto(int Id, int ProductId, int UserId, byte Rating, string Comment, DateTime CreatedAt);
}