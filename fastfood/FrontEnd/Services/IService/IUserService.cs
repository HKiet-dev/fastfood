using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface IUserService
    {
        Task<ResponseDto>? GetAll(int page = 1, int pageSize = 10);
        Task<ResponseDto>? GetById(string id);
        Task<ResponseDto>? Create(User user);
        Task<ResponseDto>? Update(User user);
        Task<ResponseDto>? Delete(string id);
        Task<ResponseDto>? GetBySearch(string query, int page = 1, int pageSize = 10);

    }
}
