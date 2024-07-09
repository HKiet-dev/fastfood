using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class CartDetail
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }
        public int Quantiy { get; set; }
        [Column( TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
