﻿using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using static Azure.Core.HttpHeader;

namespace BackEnd.Repository.Services
{
    public class CategoryService(ApplicationDbContext db, IMapper mapper) : ICategoryService
    {
        readonly ApplicationDbContext _db = db;
        readonly IMapper _mapper = mapper;
        readonly ResponseDto response = new ResponseDto();
        public ResponseDto Create(Category category)
        {
            try
            {
                _db.Category.Add(category);
                _db.SaveChanges();
                response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto Delete(int id)
        {
            try
            {
                var category = _db.Category.Find(id);
                _db.Category.Remove(category);
                _db.SaveChanges();
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "Category ID is not exist";
            }
            return response;
        }

        public ResponseDto GetAll()
        {
            var categories = _db.Category.ToList();
            if (categories.Any())
            {
                response.Result = _mapper.Map<List<CategoryDto>>(categories);
                return response;
            }
            response.IsSuccess = false;
            response.Message = "Don't have any coupon";
            return response;
        }

        public ResponseDto GetById(int id)
        {
            try
            {
                var category = _db.Category.Find(id);
                if (category == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Coupon ID is not exist";
                }
                response.Result = _mapper.Map<CategoryDto>(category);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto Update(Category category)
        {
            try
            {
                _db.Entry(category).State = EntityState.Modified;
                _db.SaveChanges();
                response.Result = _mapper.Map<CategoryDto>(category);
                response.Message = "Updated";
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "This category is not exist";
            }
            return response;
        }
    }
}