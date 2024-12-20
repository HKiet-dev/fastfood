﻿using AutoMapper;
using BackEnd.Data;
using BackEnd.Migrations;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace BackEnd.Repository.Services
{
    public class UserService(
        ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> rolemanager) : IUserService
    {
        private readonly RoleManager<IdentityRole> _roleManager = rolemanager;
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ResponseDto response = new ResponseDto();

        public async Task<ResponseDto> ChangePassword(ChangePassDto changePass)
        {
            try
            {
                if (changePass.id == null || changePass.id == "")
                {
                    response.IsSuccess = false;
                    response.Message = "Id người dùng không được trống";
                }
                var user = await _context.Users.FindAsync(changePass.id);
                var result = await _userManager.ChangePasswordAsync(user, changePass.oldPassword, changePass.newPassword);
                if (result.Succeeded)
                {
                    response.IsSuccess = true;
                    response.Message = "Đổi mật khẩu thành công";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Đổi mật khẩu thất bại";
                }
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> Create(User user, string? randomPassword)
        {
            try
            {
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Người dùng không được rỗng";
                }
                else
                {
                    user.UserName = user.Name;
                    user.NormalizedEmail = user.Email.ToUpper();
                    user.NormalizedUserName = user.UserName.ToUpper();
                    IdentityResult result;


                    if (randomPassword == null)
                    {
                        result = await _userManager.CreateAsync(user);
                    }
                    else
                    {
                        result = await _userManager.CreateAsync(user, randomPassword);
                    }

                    string role = "CUSTOMER";

                    if (!_roleManager.RoleExistsAsync(role).GetAwaiter().GetResult())
                    {
                        _roleManager.CreateAsync(new IdentityRole(role)).GetAwaiter().GetResult();
                    }

                    await _userManager.AddToRoleAsync(user, role);


                    if (result.Succeeded)
                    {
                        response.Message = "Đã tạo người dùng thành công";
                        //await _context.CartDetail.AddAsync(_mapper.Map<CartDetail>(cartDto));
                        response.Result = _mapper.Map<UserDto>(user);
                        await _context.SaveChangesAsync();
                    }
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }

            return response;
        }

        public async Task<ResponseDto> Delete(string id)
        {
            try
            {
                if (id == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Id này không được rỗng";

                }
                var user = _context.Users.FindAsync(id);
                if (await user == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Id này không tồn tại";

                }
                _context.Users.Remove(await user);
                await _context.SaveChangesAsync();
                response.Message = "Đã xóa người dùng thành công";

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                var users = await _context.Users.Where(u => u.Status == UserStatus.Active).ToListAsync();
                var totalCount = users.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var usersPerPage = users
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Users = _mapper.Map<List<UserDto>>(usersPerPage)
                };
                response.Message = "Lấy người dùng thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Lấy người dùng thất bại. Lỗi: '{ex.Message}'";
            }
            return response;
        }

        public async Task<ResponseDto> GetById(string id)
        {
            try
            {
                var user = await _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Người dùng id = '{id}' không tồn tại";
                    return response;
                }
                response.Message = $"Lấy người dùng từ id: '{id}' thành công";
                response.Result = _mapper.Map<UserDto>(user);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> GetBySearch(string query, int page, int pageSize)
        {
            try
            {
                var users = await _context.Users.Where(p => p.Status == UserStatus.Active && p.Name.Contains(query)).ToListAsync();
                var totalCount = users.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var usersPerPage = users
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Users = _mapper.Map<List<UserDto>>(usersPerPage)
                };
                response.Message = "Tìm người dùng thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<ResponseDto> Update(User user)
        {
            try
            {
                var existingUser = await _context.Users.FindAsync(user.Id);
                if (existingUser != null)
                {
                    var newUser = _mapper.Map(user, existingUser);
                    newUser.UserName = user.Name;
                    newUser.NormalizedUserName = user.Name.ToUpper();
                    newUser.NormalizedEmail = user.Email.ToUpper();
                    newUser.PasswordHash = existingUser.PasswordHash;
                    _context.Entry(existingUser).CurrentValues.SetValues(newUser);
                    await _context.SaveChangesAsync();

                    response.IsSuccess = true;
                    response.Result = _mapper.Map<UserDto>(newUser);
                    response.Message = "Đã cập nhật người dùng thành công";
                }
                else
                {
                    response.IsSuccess = false;
                    response.Message = "Không tìm thấy người dùng để cập nhật";
                }

            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Lỗi: {ex.Message}";
            }

            return response;
        }

    }
}
