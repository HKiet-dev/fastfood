using FrontEnd.Models;
using FrontEnd.Services.IService;
using System.Xml.Linq;
using static FrontEnd.Utility.StaticDetails;

namespace FrontEnd.Services
{
    public class CategoryService(IBaseService baseService) : ICategoryService
    {
        readonly IBaseService _baseService = baseService;
        public async Task<ResponseDto?> Create(Category category)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = category,
                Url = CategoryApiBase
            });
        }

        public async Task<ResponseDto?> Delete(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                Url = CategoryApiBase + "/" + id
            });
        }

        public async Task<ResponseDto?> GetAll()
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = CategoryApiBase
            });
        }

        public async Task<ResponseDto?> GetById(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = CategoryApiBase + "/" +id
            });
        }

        public async Task<ResponseDto?> GetByName(string name)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = CategoryApiBase + "/" + name
            });
        }

        public async Task<ResponseDto?> Update(Category category)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Url = CategoryApiBase
            });
        }
    }
}
