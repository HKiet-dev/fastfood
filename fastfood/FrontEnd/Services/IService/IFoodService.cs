using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface IFoodService
    {
        Task<ResponseDto>? GetAll(int page = 1, int pageSize = 10);
        Task<ResponseDto>? GetById(int id);
        Task<ResponseDto>? Create(Product product);
        Task<ResponseDto>? Update(Product product);
        Task<ResponseDto>? Delete(int id);
        Task<ResponseDto>? GetByCategoryId(int categoryid, int page = 1, int pageSize = 10);
        Task<ResponseDto>? GetBySearch(string query, int page = 1, int pageSize = 10);
        Task<ResponseDto>? GetByFilter(decimal? minrange, decimal? maxrange, int page = 1, int pageSize = 10);
        Task<ResponseDto>? TopViewed(int page = 1, int pageSize = 10);
        Task<ResponseDto>? Sorting(string sort, int page = 1, int pageSize = 10);
        Task<int>? Count();
    }
}
