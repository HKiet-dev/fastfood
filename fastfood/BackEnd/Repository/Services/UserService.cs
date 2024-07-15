using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Extensions;

namespace BackEnd.Repository.Services
{
    public class UserService(
        ApplicationDbContext context, IMapper mapper, UserManager<User> userManager, IHelper helper) : IUserService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        private readonly ResponseDto response = new ResponseDto();
        public async Task<ResponseDto> Create(User input, string? randomPassword)
        {
            try
            {
                if (input == null)
                {
                    response.IsSuccess = false;
                    response.Message = "The user parameter can not null";
                }
                else
                {
                    if (randomPassword == null)
                    {
                        await _userManager.CreateAsync(input);
                    }
                    else
                    {
                        await _userManager.CreateAsync(input, randomPassword);
                    }
                    response.Message = "Create success";
                    response.Result = _mapper.Map<User>(input);
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
                    response.Message = "The id parameter can not null";

                }
                var user = _context.Users.FindAsync(id);
                if (await user == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"User id {id} is not exist";

                }
                _context.Users.Remove(await user);
                await _context.SaveChangesAsync();
                response.Message = "Delete success";

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
                var users = _context.Users.Where(u => u.Status == UserStatus.Active).ToListAsync();
                var totalCount = users.Result.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var usersPerPage = users.Result
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = _mapper.Map<List<UserDto>>(usersPerPage);
                response.Message = "Get users success";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = $"Get user fail. Error: '{ex.Message}'";
            }
            return response;
        }

        public async Task<ResponseDto> GetById(string id)
        {
            try
            {
                var user = _context.Users.FindAsync(id);
                if (user == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"User id = {id} do not exist";
                    return response;
                }
                response.Message = $"Get user by id: {id} success";
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
                var users = _context.Users.Where(p => p.Status == UserStatus.Delete && p.Name.Contains(query)).ToList();
                var totalCount = users.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = users
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = _mapper.Map<List<UserDto>>(productsPerPage);
                response.Message = "Find user success";
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
                _context.Entry(user).State = EntityState.Modified;
                _context.SaveChanges();
                response.Result = _mapper.Map<UserDto>(user);
                response.Message = "User Updated";
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "This user is not exist";
            }
            return response;
        }
    }
}
