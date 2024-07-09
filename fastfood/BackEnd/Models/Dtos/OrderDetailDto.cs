using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.Dtos
{
    public class OrderDetailDto
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        [Range(0, double.MaxValue, ErrorMessage ="Giá sản phẩm phải lớn hơn bằng 0")]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        [Range(1,100,ErrorMessage = "Giảm giá chỉ có thể trong khoảng từ 1 đến 100")]
        public int Discount { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; set; }
    }
}
