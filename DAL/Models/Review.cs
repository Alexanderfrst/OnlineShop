namespace DAL.Models
{
    public record Review
    {
        public int Id { get; init; }
        public int ProductId { get; init; }
        public int UserId { get; init; }
        public byte Rating { get; init; }     // 1..5
        public string Comment { get; init; }
        public DateTime CreatedAt { get; init; }

        public Product Product { get; init; }
        public User User { get; init; }
    }
}
