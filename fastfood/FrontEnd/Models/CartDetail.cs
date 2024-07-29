using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class CartDetail
    {
        public string UserId {  get; set; } 
        public int ProductId { get; set; }
        [Required, Range(1, 100)]
        public int Quantity { get; set; }
        
    }
}
