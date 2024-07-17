using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Dtos
{
    public class CartDto
    {
        public int CartId { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
