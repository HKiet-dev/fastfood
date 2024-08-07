

using FrontEnd.Models;

namespace FrontEnd.Services.IService
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginRequestDto loginRequestDTO);
        Task<ResponseDto?> RegisterAsync(RegistrationRequestDto registrationRequestDTO);
        Task<ResponseDto?> AssignRoleAsync(RegistrationRequestDto registrationRequestDTO);
        Task<ResponseDto?> CreateUserFromGoogleLogin(UserDto user);
        Task<ResponseDto?> GetUserRole(UserDto user);
        Task<ResponseDto?> GetUserId(string email);
        Task<ResponseDto?> GenerateJwt(UserDto user);
        Task<ResponseDto?> GetUserById(string userId);
        Task<ResponseDto?> ForgotPassword(string email);
    }
}
