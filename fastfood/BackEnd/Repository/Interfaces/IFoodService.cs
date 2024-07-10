using BackEnd.Models;
using BackEnd.Models.Dtos;

namespace BackEnd.Repository.Interfaces
{
    public interface IFoodService
    {
        ResponseDto GetAll();
        ResponseDto GetById(int id);
        ResponseDto Create(Product product);
        ResponseDto Update(Product product);
        ResponseDto Delete(int id);
        ResponseDto GetByCategoryId(int categoryid);
        ResponseDto GetBySearch(string query);
        ResponseDto GetByFilter(int? categoryid,decimal? price, bool IsActive = true );
        ResponseDto TopViewed();

    }
}
