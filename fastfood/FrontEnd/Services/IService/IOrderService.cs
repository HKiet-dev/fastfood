using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface IOrderService
    {
        Task<ResponseDto?> Payment(Order order);
        Task<ResponseDto> MomoPayment(string amount);
        Task<ResponseDto> VNPayment(int amount);
        Task<ResponseDto> GetAll();
        Task<ResponseDto> GetOrderByID(int orderId);
        Task<ResponseDto> GetOrderDetail(int orderId);
        Task<ResponseDto> UpdateOrderStatus(int orderid, string message);
    }
}
