using BackEnd.Models;
using BackEnd.Models.Dtos;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
namespace BackEnd.Repository.Interfaces
{
    public interface ICartService
    {
        ResponseDto GetAll(); 
        ResponseDto CreateCart(CartDetail cartDetail);
        ResponseDto UpdateCart(CartDetail cartDetail);
        ResponseDto DeleteCart(int  prodcutId, string userId);
        ResponseDto DeleteAllById (string userId);
        ResponseDto GetById (string userId);
        ResponseDto getCart(string UserId);
    }
}
