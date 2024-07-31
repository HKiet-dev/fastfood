using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class Order
    {
        [Required(ErrorMessage = "Họ và tên không được rỗng")]
        public string FullName { get; set; } = string.Empty;
        [Required(ErrorMessage = "Địa chỉ không được rỗng"), MaxLength(250,ErrorMessage = "Độ dài tối đa 250 kí tự")]
        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "Số điện thoại không được rỗng"), RegularExpression("^(03|06|07|08|09)[0-9]{8}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required(ErrorMessage = "Vui lòng chọn phương thức thanh toán")]
        public string PaymentType { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = "Chưa thanh toán";
        public string? note { get; set; }
    }
}
