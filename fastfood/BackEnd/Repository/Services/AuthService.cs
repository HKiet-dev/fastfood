﻿using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
#pragma warning disable 1591
namespace BackEnd.Repository.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public AuthService(ApplicationDbContext context, 
                           UserManager<User> userManager, 
                           RoleManager<IdentityRole> roleManager, 
                           IJwtTokenGenerator jwtTokenGenrator, 
                           IMapper mapper,SignInManager<User> signInManager,
                           IHelper helper)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _jwtTokenGenerator = jwtTokenGenrator;
            _mapper = mapper;
            _helper = helper;
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

        public async Task<User> CreateUserFromGoogleLogin(User user)
        {
            var eUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

            if (eUser != null)
            {
                await _signInManager.PasswordSignInAsync(eUser, "", false, false);
                return eUser;
            }

            user = new User
            {

                Avatar = user.Avatar,
                UserName = user.Email,
                Email = user.Email,
                Name = user.Name,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
            };

            string role = "CUSTOMER";
            var result = await _userManager.CreateAsync(user);
            if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
            }

            await _userManager.AddToRoleAsync(user, role);
            await _signInManager.PasswordSignInAsync(user, "", false, false);
            if (result.Succeeded)
            {
                return user;
            }

            return null;
        }

		public async Task<string> ForgotPassword(string email)
		{
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            return await _userManager.GeneratePasswordResetTokenAsync(user);
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
            var user = await _userManager.FindByNameAsync(loginRequestDTO.UserName);
            bool isValid = await _userManager.CheckPasswordAsync(user, loginRequestDTO.Password);

            if (user == null || isValid == false)
            {
                return new LoginResponseDto() { User = null, Token = null, Role = null };
            }
            // first params : người dùng có tên đăng nhập là loginRequestDTO.UserName (object)
            // second params : mật khẩu mà người dùng nhập  (string)
            // third params : ghi nhớ trạng thái đăng nhập (bool)
            // fourth params : mặc định nếu đăng nhập sai 5 lần liên tiếp sẽ bị khoá đăng nhập trong 5 phút
            var result = await _signInManager.PasswordSignInAsync(user, loginRequestDTO.Password, false, false);

            if (!result.Succeeded)
            {
                return new LoginResponseDto() { User = null, Token = null, Role = null };
            }

            var roles = await GetUserRole(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);

            var userDto = _mapper.Map<UserDto>(user);

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
                PhoneNumber = registrationRequestDTO.PhoneNumber,
                Address = registrationRequestDTO.Address,
                Avatar = registrationRequestDTO.Avatar,
                Gender = registrationRequestDTO.Gender,
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
                        PhoneNumber = userToReturn.PhoneNumber,
                        Address = userToReturn.Address,
                        Avatar = userToReturn.Avatar,
                        Gender = userToReturn.Gender,
                    };

                    string role = "CUSTOMER";

                    if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                    }

                    await _userManager.AddToRoleAsync(user, role);

                    return "Tạo tài khoản thành công";
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
