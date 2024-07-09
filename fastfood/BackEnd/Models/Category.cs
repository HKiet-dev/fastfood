using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class Category
    {
        public int Id { get; set; }
        [RegularExpression(@"^[\p{L}\p{M} 0-9]+$", ErrorMessage = "Tên loại sản phẩm phải là chữ hoặc số")]
        [Required(ErrorMessage = "Tên loại sản phẩm là bắt buộc")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Tên loại sản phẩm phải từ 2 đến 50 ký tự")]
        public required string Name { get; set; }
        public List<Product>? Products { get; set; }
    }
}
