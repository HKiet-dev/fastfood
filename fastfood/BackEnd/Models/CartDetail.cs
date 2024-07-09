using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class CartDetail
    {
        public string UserId { get; set; }
        public int ProductId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Số lượng phải lớn hơn 0")]
        public int Quantity { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
