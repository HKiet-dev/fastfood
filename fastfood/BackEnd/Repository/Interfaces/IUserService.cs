﻿using BackEnd.Models;
using BackEnd.Models.Dtos;

namespace BackEnd.Repository.Interfaces
{
    public interface IUserService
    {
        Task<ResponseDto> GetAll(int page, int pageSize);
        Task<ResponseDto> GetById(string id);
        Task<ResponseDto> Create(User user, string? randomPassword);
        Task<ResponseDto> Update(User user);
        Task<ResponseDto> Delete(string id);
        Task<ResponseDto> GetBySearch(string query, int page = 1, int pageSize = 10);
        Task<ResponseDto> ChangePassword(ChangePassDto changePass);
    }
}
