using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Dtos
{
    public class UserDto
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Phone]
        public string Phone {  get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Gender { get; set; }
        public string Address { get; set; }
        public string Avatar { get; set; }
    }
}
