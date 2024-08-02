using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên sản phẩm là bắt buộc")]
        [MaxLength(50, ErrorMessage = "Tên phải ít hơn 50 ký tự")]
        public string Name { get; set; } 
        [MaxLength(1000, ErrorMessage ="Mô tả phải ít hơn 1000 ký tự")]
        public string Description { get; set; } = string.Empty;
        [Column(TypeName = "DECIMAL(18,2)")]
        [Range(10000,double.MaxValue, ErrorMessage ="Giá phải lớn hơn hoặc bằng 10.000")]
        public decimal Price { get; set; }
        public int View { get; set; } = 0;
        public bool IsActive { get; set; }
        public bool IsCombo { get; set; }
        public string? QR { get; set; }
        public string ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
