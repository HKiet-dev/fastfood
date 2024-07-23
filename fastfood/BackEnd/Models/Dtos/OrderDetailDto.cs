using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace BackEnd.Models.Dtos
{
    public class OrderDetailDto
    {
        public string Name { get; set; }
        [Required, Range(1000, 1000000), Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required, Range(1000, 1000000), Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; }
    }
}
