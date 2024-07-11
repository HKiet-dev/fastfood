using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(1000, ErrorMessage ="Mô tả phải ít hơn 1000 ký tự")]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "DECIMAL(18,2)")]
        [Range(0,double.MaxValue, ErrorMessage ="Giá phải lớn hơn hoặc bằng 0")]
        public decimal Price { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Số lượt xem phải lớn hơn hoặc bằng 0")]
        public int View { get; set; }
        public bool IsActive { get; set; }
        public bool IsCombo { get; set; }
        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public List<CartDetail> CartDetails { get; set; }
    }
}
