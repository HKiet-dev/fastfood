using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "Không được để trống tài khoản")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Không được để trống mật khẩu")]
        public string Password { get; set; }
    }
}
