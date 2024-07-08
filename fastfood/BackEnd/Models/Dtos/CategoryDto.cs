using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models.Dtos
{
    public class CategoryDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
