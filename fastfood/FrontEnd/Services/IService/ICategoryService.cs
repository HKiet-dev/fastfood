using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface ICategoryService
    {
        Task<ResponseDto?> GetAll();
        Task<ResponseDto?> GetById(int id);
        Task<ResponseDto?> Create(Category category);
        Task<ResponseDto?> Update(Category category);
        Task<ResponseDto?> Delete(int id);
        Task<ResponseDto?> GetByName(string name);
    }
}
