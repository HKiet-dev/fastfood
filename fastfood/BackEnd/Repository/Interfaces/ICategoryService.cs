using BackEnd.Models;
using BackEnd.Models.Dtos;

namespace BackEnd.Repository.Interfaces
{
    public interface ICategoryService
    {
        ResponseDto GetAll();
        ResponseDto GetById(int id);
        ResponseDto Create(Category category);
        ResponseDto Update(Category category);
        ResponseDto Delete(int id);
        ResponseDto GetByName(string name);
    }
}
