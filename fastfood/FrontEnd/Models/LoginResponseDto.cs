namespace FrontEnd.Models
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public IList<string>? Role { get; set; }
        public string Token { get; set; }
    }
}
