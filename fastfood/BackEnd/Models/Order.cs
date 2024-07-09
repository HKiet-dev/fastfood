using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateReceived { get; set; }
        public string FullName { get; set; }
        public string PhoneNo { get; set; }
        public string Address { get; set; }
        public string PaymentType { get; set; }
        public string PaymentStatus { get; set; }
        public string OrderStatus { get; set; }
        public string Note { get; set; }
        public DateTime CreateAt { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; }
        public List<OrderDetail> OrderDetails { get; set; }
    }
}
