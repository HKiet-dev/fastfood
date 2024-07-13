using BackEnd.Models.Dtos;

namespace BackEnd.Repository.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto> GetAll();
        Task<ResponseDto> GetById(string id);
        Task<ResponseDto> Create(UserDto user);
        Task<ResponseDto> Update(UserDto user);
        Task<ResponseDto> Delete(string id);
        Task<ResponseDto> GetBySearch(string query, int page = 1, int pageSize = 10);
    }
}
