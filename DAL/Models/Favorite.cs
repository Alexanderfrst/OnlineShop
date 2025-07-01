namespace DAL.Models
{
    public class Favorite
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public DateTime AddedAt { get; set; }

        public User User { get; set; }
        public Product Product { get; set; }
    }
}