using AutoMapper;
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
                response.Message = "Category Created";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
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
                response.Message = "Category Deleted";
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "The ID is not exist";
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
            response.Message = "Don't have any category";
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
                    response.Message = "The ID is not exist";
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

        public ResponseDto GetByName(string name)
        {
            try
            {
                var categories = _db.Category.Where(x=> x.Name.Contains(name)).ToList();
                if (!categories.Any())
                {
                    response.IsSuccess = false;
                    response.Message = $"The name '{name}' is not exist";
                }
                response.Result = _mapper.Map<List<CategoryDto>>(categories);
            } catch (Exception ex)
            {
                response.IsSuccess=false;
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
                response.Message = "Category Updated";
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
