using System;

namespace DAL.Models
{
    public class PromoCode
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public decimal DiscountValue { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsActive { get; set; }
    }
}