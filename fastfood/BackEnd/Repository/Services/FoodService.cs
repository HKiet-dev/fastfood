using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repository.Services
{
    public class FoodService(ApplicationDbContext db, IMapper mapper) : IFoodService
    {
        readonly ApplicationDbContext _db = db;
        readonly IMapper _mapper = mapper;
        readonly ResponseDto response = new ResponseDto();
        public ResponseDto Create(Product product)
        {
            try
            {
                if(product == null)
                {
                    response.IsSuccess = false;
                    response.Message = "The product parameter can not null";
                    return response;
                }
                if(_db.Product.Any(p => p.Name == product.Name))
                {
                    response.IsSuccess = false;
                    response.Message = $"The product with name {product.Name} already exist";
                    return response;
                }
                _db.Product.Add(product);
                _db.SaveChanges();
                response.Result = _mapper.Map<ProductDto>(product);
                response.Message = "Product Created";
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
                var product = _db.Product.Find(id);

                // Check exist
                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = "Product do not exist";
                    return response;
                }

                _db.Product.Remove(product);
                _db.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Product Deleted";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetAll()
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive).ToList();
                response.Result = _mapper.Map<List<ProductDto>>(products);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetByCategoryId(int categoryid)
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive && p.CategoryId == categoryid).ToList();
                response.Result = _mapper.Map<List<ProductDto>>(products);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetByFilter(int? categoryid, decimal? price)
        {
            try
            {
                var products = _db.Product.Where(x => x.IsActive).ToList();
                if (categoryid.HasValue)
                {
                    products = products.Where(x => x.CategoryId == categoryid).ToList();
                }
                if (price.HasValue)
                {
                    products = products.Where(x => x.Price >= price).ToList();
                }
                response.Result = products;
            } catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetById(int id)
        {
            try
            {
                var product = _db.Product.Find(id);
                response.Result = _mapper.Map<ProductDto>(product);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetBySearch(string query)
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive && p.Name.Contains(query)).ToList();
                response.Result = _mapper.Map<List<ProductDto>>(products);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto TopViewed()
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive).OrderByDescending(x => x.View).ToList();
                response.Result = _mapper.Map<List<ProductDto>>(products);
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto Update(Product product)
        {
            try
            {
                _db.Entry(product).State = EntityState.Modified;
                _db.SaveChanges();
                response.Result = _mapper.Map<ProductDto>(product);
                response.Message = "Product Updated";
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = "This product is not exist";
            }
            return response;
        }
    }
}
