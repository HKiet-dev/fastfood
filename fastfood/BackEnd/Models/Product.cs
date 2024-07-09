using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Price { get; set; }
        public int View { get; set; }
        public bool IsActive { get; set; }
        public bool IsCombo { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public DateTime CreateAt { get; set; }
        public List<CartDetail> CartDetails { get; set; }
    }
}
