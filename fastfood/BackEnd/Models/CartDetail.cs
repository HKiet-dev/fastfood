using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class CartDetail
    {
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantiy { get; set; }
        [Column( TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; set; }
        public Product Product { get; set; }
        public Cart Cart { get; set; }
    }
}
