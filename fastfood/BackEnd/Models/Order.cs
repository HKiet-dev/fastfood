using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace BackEnd.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }
        [MaxLength(450), ForeignKey("User")]
        public string UserId { get; set; }
        [Column(TypeName = "datetime2")]
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public DateTime? ReceviedDate { get; set; }
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required, MaxLength(250)]
        public string Address { get; set; } = string.Empty;

        [Required]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string PaymentType { get; set; } = string.Empty;
        [Required]
        public string PaymentStatus { get; set; } = string.Empty;
        [Required]
        public string OrderStatus { get; set; } = "Đang chuẩn bị";
        public string? note { get; set; } = string.Empty;

        // Navigation
        public virtual User User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
