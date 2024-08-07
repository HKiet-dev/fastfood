using FrontEnd.Models;
using FrontEnd.Services.IService;
using FrontEnd.Utility;

namespace FrontEnd.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;
        public AuthService(IBaseService baseService) 
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = registrationRequestDTO,
                Url = StaticDetails.ApiUrl + "/api/Auth/assignRole"
            });
        }

        public async Task<ResponseDto?> CreateUserFromGoogleLogin(UserDto user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.ApiUrl + "/api/Auth/googleaccount"
            });
        }

        public async Task<ResponseDto?> GenerateJwt(UserDto user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Data = user,
                Url = StaticDetails.ApiUrl + "/api/Auth/jwtauth"
            });
        }

        public async Task<ResponseDto?> GetUserId(string email)
        {

            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.ApiUrl + "/api/Auth/userid?email=" + email
            });
        }

        public async Task<ResponseDto?> GetUserRole(UserDto user)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = user,
                Url = StaticDetails.ApiUrl + "/api/Auth/userrole"
            });
        }

        public async Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDTO)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = loginRequestDTO,
                Url = StaticDetails.ApiUrl + "/api/Auth/login"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> RegisterAsync(RegistrationRequestDto obj)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Data = obj,
                Url = StaticDetails.ApiUrl + "/api/Auth/register"
            }, withBearer: false);
        }

        public async Task<ResponseDto?> GetUserById(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.GET,
                Url = StaticDetails.ApiUrl + "/api/Auth/userbyid/" + userId
            }, withBearer: false);
        }

        public async Task<ResponseDto?> ForgotPassword(string email)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = StaticDetails.ApiType.POST,
                Url = StaticDetails.ApiUrl + "/api/Auth/forgotpassword/" + email
            }, withBearer: false);
        }
    }
}
