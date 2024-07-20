using FrontEnd.Models;
using FrontEnd.Services.IService;
using static FrontEnd.Utility.StaticDetails;

namespace FrontEnd.Services
{
    public class FoodService(IBaseService baseService) : IFoodService
    {
        readonly IBaseService _baseService = baseService;
        readonly string _foodUri = ApiUrl + "/api/food";
        public async Task<ResponseDto>? Create(Product product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = product,
                Url = _foodUri
            });
        }

        public async Task<ResponseDto>? Delete(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                Url = _foodUri + $"/{id}"
            });
        }

        public async Task<ResponseDto>? GetAll(int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _foodUri + $"?page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? GetByCategoryId(int categoryid, int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _foodUri + $"/{categoryid}?page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? GetByFilter(int? categoryid, decimal? price, int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _foodUri + $"/Filter?categoryid={categoryid}&price={price}&page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? GetById(int id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _foodUri + $"/{id}"
            });
        }

        public async Task<ResponseDto>? GetBySearch(string query, int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _foodUri + $"/Get-by-search?query={query}&page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? TopViewed(int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _foodUri + $"/Get-top-view?page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? Update(Product product)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Data = product,
                Url = _foodUri
            });
        }
    }
}
