namespace BLL.DTO
{
    public record PromoCodeDto(int Id, string Code, decimal DiscountValue, DateTime ExpiryDate, bool IsActive);
}