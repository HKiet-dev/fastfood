using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Dtos
{
    public class LoginResponseDto
    {
        [Required(ErrorMessage = "Không tìm thấy User")]
        public UserDto User { get; set; }
        public IList<string>? Role { get; set; }
        public string Token { get; set; }
    }
}
