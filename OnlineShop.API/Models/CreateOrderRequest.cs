namespace OnlineShop.API.Models
{
    public record CreateOrderRequest(int UserId, string DeliveryMethod, string PromoCode);
}