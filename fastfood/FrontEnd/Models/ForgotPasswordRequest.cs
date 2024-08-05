using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ForgotPasswordRequest
    {
        [Required(ErrorMessage = "Hãy điền email của bạn")]
        public string Email { get; set; }
    }
}
