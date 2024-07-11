using BackEnd.Models;
using BackEnd.Models.Dtos;

namespace BackEnd.Repository.Interfaces
{
    public interface IFoodService
    {
        ResponseDto GetAll(int page = 1, int pageSize = 10);
        ResponseDto GetById(int id);
        ResponseDto Create(Product product);
        ResponseDto Update(Product product);
        ResponseDto Delete(int id);
        ResponseDto GetByCategoryId(int categoryid, int page = 1, int pageSize = 10);
        ResponseDto GetBySearch(string query, int page = 1, int pageSize = 10);
        ResponseDto GetByFilter(int? categoryid,decimal? price, int page = 1, int pageSize = 10);
        ResponseDto TopViewed(int page = 1, int pageSize = 10);

    }
}
