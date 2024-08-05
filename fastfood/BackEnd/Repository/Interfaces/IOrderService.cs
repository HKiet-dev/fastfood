using BackEnd.Models.Dtos;
using BackEnd.Models;
#pragma warning disable 1591
namespace BackEnd.Repository.Interfaces
{
    public interface IOrderService
    {
        Order AddOrder(Order order);
        void AddOrderDetail(IEnumerable<OrderDetail> orderdetails);
        ResponseDto Orders();
        ResponseDto Order(int id);
        ResponseDto getOrderId(string UserId);
        ResponseDto GetOrderDetails(int OrderId);
        decimal Profit();
        void UpdateOrderStatus(int OrderId, string message);
        void CancelOrder(int OrderId);
    }
}
