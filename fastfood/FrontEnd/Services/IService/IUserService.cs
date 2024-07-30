using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface IUserService
    {
        Task<ResponseDto>? GetAll(int page = 1, int pageSize = 10);
        Task<ResponseDto>? GetById(string id);
        Task<ResponseDto>? Create(UserDto user);
        Task<ResponseDto>? Update(UserDto user);
        Task<ResponseDto>? Delete(string id);
        Task<ResponseDto>? GetBySearch(string query, int page = 1, int pageSize = 10);

    }
}
