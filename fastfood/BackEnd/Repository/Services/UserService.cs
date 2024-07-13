using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace BackEnd.Repository.Services
{
    public class UserService(ApplicationDbContext context, IMapper mapper, UserManager<User> userManager) : IUserService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;
        private readonly UserManager<User> _userManager = userManager;
        public Task<ResponseDto> Create(UserDto input)
        {
            Random random = new Random();
            char nextChar = (char)random.Next(33, 127);
        }

        public Task<ResponseDto> Delete(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> GetBySearch(string query, int page, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseDto> Update(UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
