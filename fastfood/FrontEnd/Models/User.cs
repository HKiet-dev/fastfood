using BackEnd.Models;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class User
    {
        [Required(ErrorMessage = "Id là bắt buộc")]
        public string Id { get; set; }
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Độ dài của tên phải từ 3 đến 50 ký tự")]
        [RegularExpression("^[a-zA-Z 0-9_]+$", ErrorMessage = "Tên tài khoản phải là ký tự không dấu hoặc số")]
        public string Name { get; set; }
        [StringLength(13, MinimumLength = 9, ErrorMessage = "Độ dài số điện thoại phải từ 9 đến 13 chữ số")]
        [Phone(ErrorMessage = "Số điện thoại phải là chữ số")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Email không được để trống")]
        [EmailAddress(ErrorMessage = "Email phải đúng định dạng")]
        public string Email { get; set; }
        public int Gender { get; set; }
        [StringLength(250, MinimumLength = 5, ErrorMessage = "Độ dài địa chỉ phải từ 5 đến 250 ký tự")]
        public string Address { get; set; }
        public string Avatar { get; set; }
        [Required]
        [EnumDataType(typeof(UserStatus))]
        public UserStatus Status { get; set; } = UserStatus.Active;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    }

    public enum UserStatus
    {
        Active,
        Inactive,
        Delete,
        Block
    }
}
