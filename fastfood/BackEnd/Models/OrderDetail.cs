using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace BackEnd.Models
{
    [PrimaryKey(nameof(OrderId), nameof(ProductId))]
    public class OrderDetail
    {
        [Required, ForeignKey("Order")]
        public int OrderId { get; set; }
        [Required, ForeignKey("Product")]
        public int ProductId { get; set; }
        [Required, Range(1000, 1000000), Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, Range(1000, 1000000), Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }

        // Navigation
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}
