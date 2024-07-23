using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace BackEnd.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public DateTime? DateReceived { get; set; }
        [Required, StringLength(100, MinimumLength = 2)]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string PhoneNo { get; set; } = string.Empty;
        [Required, MaxLength(250)]
        public string Address { get; set; } = string.Empty;
        [Required]
        public string PaymentType { get; set; } = string.Empty;
        [Required]
        public string PaymentStatus { get; set; } = string.Empty;
        [Required]
        public string OrderStatus { get; set; } = "Đang chuẩn bị";
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
