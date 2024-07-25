using System.ComponentModel.DataAnnotations;
#pragma warning disable 1591
namespace BackEnd.Models.Dtos
{
    public class CreateOrderDto
    {
        [Required, MaxLength(100)]
        public string FullName { get; set; } = string.Empty;
        [Required, MaxLength(250)]
        public string Address { get; set; } = string.Empty;
        [Required, RegularExpression("^(03|06|07|08|09)[0-9]{8}$", ErrorMessage = "Số điện thoại không hợp lệ")]
        public string PhoneNumber { get; set; } = string.Empty;
        [Required]
        public string PaymentType { get; set; } = string.Empty;
        public string PaymentStatus { get; set; } = string.Empty;
        public string? note { get; set; } = string.Empty;
    }
}
