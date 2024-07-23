using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace BackEnd.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId không được để trống")]
        public string UserId { get; set; }
        public DateTime DateReceived { get; set; }
        
        [Required,RegularExpression(@"^[a-zA-ZÀ-ỹ]{2,}(\s[a-zA-ZÀ-ỹ]{2,})*$", ErrorMessage = "Họ và tên không hợp lệ")]
        public string FullName { get; set; }
        [Required ,RegularExpression("^(03|08|07|05)[0-9]{8}$",ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNo { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string PaymentType { get; set; } = string.Empty;
        [Required]
        public string PaymentStatus { get; set; } = string.Empty;
        [Required]
        public string OrderStatus { get; set; } = "Đang chuẩn bị";
        public string? Note { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
