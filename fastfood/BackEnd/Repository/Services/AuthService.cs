using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;

        public AuthService(ApplicationDbContext context, 
                           UserManager<User> userManager, 
                           RoleManager<IdentityRole> roleManager, 
                           IJwtTokenGenerator jwtTokenGenrator, 
                           IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtTokenGenerator = jwtTokenGenrator;
            _mapper = mapper;
        }

        public async Task<bool> AssignRole(string email, string roleName)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user != null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }
                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }

        public Task<User> CreateUserFromGoogleLogin(string email, string name)
        {
            throw new NotImplementedException();
        }

        public async Task<string> GenerateJwt(UserDto user)
        {
            var role = await _userManager.GetRolesAsync(_mapper.Map<User>(user));
            return _jwtTokenGenerator.GenerateToken(_mapper.Map<User>(user), role);
        }

        public async Task<UserDto> GetUserByEmail(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> GetUserById(string userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null)
            {
                return null;
            }

            return _mapper.Map<UserDto>(user);
        }

        public async Task<string> GetUserId(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user == null)
            {
                return null;
            }

            return user.Id;
        }

        public async Task<IList<string>> GetUserRole(User user)
        {
            return await _userManager.GetRolesAsync(user);
        }

        public async Task<LoginResponseDto> Login(LoginRequestDto loginRequestDTO)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName.ToLower() == loginRequestDTO.UserName.ToLower());
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = null, Role = null };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            var userDto = new UserDto()
            {
                Id = user.Id,
                Name = user.Name,
                Phone = user.PhoneNumber,
                Email = user.Email,
                Address = user.Address,
                Gender = user.Gender,
                Avatar = user.Avatar
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token,
                Role = roles
            };

            return loginResponseDto;
        }

        public async Task<string> Resgister(RegistrationRequestDto registrationRequestDTO)
        {
            User user = new User()
            {
                UserName = registrationRequestDTO.Name,
                Email = registrationRequestDTO.Email,
                NormalizedEmail = registrationRequestDTO.Email.ToUpper(),
                Name = registrationRequestDTO.Name,
                PhoneNumber = registrationRequestDTO.Phone,
                Address = registrationRequestDTO.Address,
                Avatar = registrationRequestDTO.Avatar
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registrationRequestDTO.Password);
                if (result.Succeeded)
                {
                    var userToReturn = await _context.Users.FirstOrDefaultAsync();

                    UserDto userDto = new()
                    {
                        Email = userToReturn.Email,
                        Id = userToReturn.Id,
                        Name = userToReturn.Name,
                        Phone = userToReturn.PhoneNumber,
                        Address = userToReturn.Address,
                        Avatar = userToReturn.Avatar
                    };

                    CartDto cartDto = new()
                    {
                        UserId = userToReturn.Id,
                    };

                    await _context.Cart.AddAsync(_mapper.Map<Cart>(cartDto));
                    await _context.SaveChangesAsync();

                    return "";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch
            {

            }
            return "Error Encoutered";
        }
    }
}
