namespace BackEnd.Models.Dtos
{
    public class ListCartDetail
    {
        public ProductDto Food { get; set; }
        public int Quantity { get; set; }
        public decimal Total { get; set; }
    }
}
