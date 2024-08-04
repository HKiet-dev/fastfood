using FrontEnd.Models;
namespace FrontEnd.Services.IService
{
    public interface ICartService
    {
       Task <ResponseDto?> GetAll();
       Task <ResponseDto?> CreateCart(CartDetail cartDetail);
       Task<ResponseDto?> UpdateCart(CartDetail cartDetail);
       Task<ResponseDto?> DeleteCart(int prodcutId, string userId);
       Task<ResponseDto?> DeleteAllById();
       Task<ResponseDto?> GetById(string userId);
        Task<ResponseDto?> getCart(string userId);
    }
}
