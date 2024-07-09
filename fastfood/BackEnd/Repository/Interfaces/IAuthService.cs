using BackEnd.Models;
using BackEnd.Models.Dtos;

namespace BackEnd.Repository.Interfaces
{
    public interface IAuthService
    {
        Task<string> Resgister(RegistrationRequestDto registrationRequestDTO);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDTO);
        Task<bool> AssignRole(string email, string roleName);
        Task<IList<string>> GetUserRole(User users);
        Task<string> GetUserId(string email);
        Task<UserDto> GetUserById(string userId);
        Task<string> GenerateJwt(UserDto user);
        Task<UserDto> GetUserByEmail(string email);
        Task<User> CreateUserFromGoogleLogin(string email, string name);
    }
}
