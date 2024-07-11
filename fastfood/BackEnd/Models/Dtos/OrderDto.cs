using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "UserId không được để trống")]
        public string UserId { get; set; }
        public DateTime DateReceived { get; set; }
        //Ký tự có dấu, khoảng trắng
        [RegularExpression(@"^[\p{L}\p{M} ]+$", ErrorMessage = "Tên đầy đủ phải là chữ")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "Tên đầy đủ phải từ 2 đến 100 ký tự")]
        public string FullName { get; set; }
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Độ dài số điện thoại phải từ 9 đến 13 chữ số")]
        [Phone(ErrorMessage = "Số điện thoại phải là chữ số")]
        public string PhoneNo { get; set; }
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Độ dài địa chỉ phải từ 5 đến 250 ký tự")]
        public string Address { get; set; }
        [StringLength(50, ErrorMessage = "Độ dài loại hình thanh toán tối đa 50 ký tự")]
        public string PaymentType { get; set; }
        [StringLength(50, ErrorMessage = "Độ dài trạng thái thanh toán tối đa 50 ký tự")]
        public string PaymentStatus { get; set; }
        [StringLength(50, ErrorMessage ="Độ dài trạng thái hóa đơn tối đa 50 ký tự")]
        public string OrderStatus { get; set; }
        [MaxLength(1000, ErrorMessage = "Độ dài ghi chú tối đa 1000 ký tự")]
        public string Note { get; set; }
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }
}
