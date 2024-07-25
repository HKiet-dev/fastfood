using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
namespace BackEnd.Models.Dtos
{
    public class CartDetailDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required, Range(1, 100)]
        public int Quantity { get; set; }
    }
}
