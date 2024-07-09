using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateReceived { get; set; }
        [StringLength(50)]
        public string FullName { get; set; }
        [Phone]
        public string PhoneNo { get; set; }
        [StringLength(100)]
        public string Address { get; set; }
        [StringLength(50)]
        public string PaymentType { get; set; }
        [StringLength(50)]
        public string PaymentStatus { get; set; }
        [StringLength(50)]
        public string OrderStatus { get; set; }
        [StringLength(100)]
        public string Note { get; set; }
        public DateTime CreateAt { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
