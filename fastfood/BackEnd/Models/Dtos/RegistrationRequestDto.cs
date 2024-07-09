using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Dtos
{
    public class RegistrationRequestDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [Phone]
        public string Phone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}
