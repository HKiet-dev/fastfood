using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
#pragma warning disable 1591
#pragma warning disable 8618
namespace BackEnd.Models.Dtos
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public string FullName { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string PaymentType { get; set; }
        public string? note { get; set; }
        public DateTime OrderDate { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
    }
}
