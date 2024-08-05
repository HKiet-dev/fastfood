using FrontEnd.Models;
using FrontEnd.Services.IService;
using static FrontEnd.Utility.StaticDetails;

namespace FrontEnd.Services
{
    public class UserService(IBaseService baseService) : IUserService
    {
        readonly IBaseService _baseService = baseService;
        readonly string _userUri = ApiUrl + "/api/user";
        public async Task<ResponseDto>? Create(UserDto user)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = user,
                Url = _userUri
            });
        }

        public async Task<ResponseDto>? Delete(string id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.DELETE,
                Url = _userUri + $"/{id}"
            });
        }

        public async Task<ResponseDto>? GetAll(int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _userUri + $"?page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? GetById(string id)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _userUri + $"/{id}"
            });
        }

        public async Task<ResponseDto>? GetBySearch(string query, int page = 1, int pageSize = 10)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.GET,
                Url = _userUri + $"/search?query={query}&page={page}&pageSize={pageSize}"
            });
        }

        public async Task<ResponseDto>? Update(UserDto user)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.PUT,
                Data = user,
                Url = _userUri
            });
        } 
        public async Task<ResponseDto>? ChangePassword(ChangePassDto changePass)
        {
            return await _baseService.SendAsync(new RequestDto
            {
                ApiType = ApiType.POST,
                Data = changePass,
                Url = _userUri + "/changepassword"
            });
        }
    }
}
