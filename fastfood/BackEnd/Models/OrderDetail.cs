using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class OrderDetail
    {
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        [Range(0, double.MaxValue)]
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
/*        [Range(1,100,ErrorMessage = "Giảm giá chỉ có thể trong khoảng từ 1 đến 100")] chỉ nên dùng anotation trong Dto */
        public int Discount { get; set; }
        [Column(TypeName = "DECIMAL(18,2)")]
        public decimal Total { get; set; }
        [ForeignKey(nameof(ProductId))]
        public Product Product { get; set; }
        [ForeignKey(nameof(OrderId))]
        public Order Order { get; set; }
    }
}
