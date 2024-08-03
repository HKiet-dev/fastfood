using AutoMapper;
using BackEnd.Data;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;
#pragma warning disable 1591
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
                    response.Message = "Thức ăn truyền vào không hợp lệ";
                    return response;
                }
                if(_db.Product.Any(p => p.Name == product.Name))
                {
                    response.IsSuccess = false;
                    response.Message = $"Thức ăn tên : {product.Name} đã tồn tại";
                    return response;
                }
                _db.Product.Add(product);
                _db.SaveChanges();
                response.Result = _mapper.Map<ProductDto>(product);
                response.Message = "Thêm thức ăn thành công";
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
                    response.Message = "Thức ăn này không tồn tại";
                    return response;
                }

                _db.Product.Remove(product);
                _db.SaveChanges();
                response.IsSuccess = true;
                response.Message = "Xoá thức ăn thành công";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetAll(int page = 1, int pageSize = 10)
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive).ToList();
                var totalCount = products.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

                var productsPerPage = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                response.Result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Products = _mapper.Map<List<ProductDto>>(productsPerPage)
                };
                response.IsSuccess = true;
                response.Message = "Lấy danh sách sản phẩm thành công.";
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
            //try
            //{
            //    var products = _db.Product.Where(p => p.IsActive).ToList();
            //    var totalCount = products.Count;
            //    var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
            //    var productsPerPage = products
            //        .Skip((page - 1) * pageSize)
            //        .Take(pageSize)
            //        .ToList();
            //    response.Result = _mapper.Map<List<ProductDto>>(productsPerPage);
            //    response.IsSuccess = true;
            //}
            //catch (Exception ex)
            //{
            //    response.IsSuccess = false;
            //    response.Message = ex.Message;
            //}
            //return response;
        }

        public ResponseDto GetByCategoryId(int categoryid, int page = 1, int pageSize = 10)
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive && p.CategoryId == categoryid).ToList();
                var totalCount = products.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Products = _mapper.Map<List<ProductDto>>(productsPerPage)
                };
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetByFilter(decimal? minrange, decimal? maxrange, int page = 1, int pageSize = 10)
        {
            try
            {
                var products = _db.Product.Where(x => x.IsActive).ToList().AsQueryable();
                if (minrange.HasValue && maxrange.HasValue)
                {
                    products = products.Where(x => x.Price <= maxrange.Value && x.Price >= minrange.Value);
                }
                var productFiltered = products.ToList();
                var totalCount = productFiltered.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = productFiltered
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Products = _mapper.Map<List<ProductDto>>(productsPerPage)
                };
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
                if (product == null)
                {
                    response.IsSuccess = false;
                    response.Message = $"Thức ăn này không tồn tại";
                    return response;
                }
                product.View += 1;
                _db.SaveChanges();
                response.Result = _mapper.Map<ProductDto>(product);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto GetBySearch(string query, int page = 1, int pageSize = 10)
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive && p.Name.Contains(query)).ToList();
                var totalCount = products.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = _mapper.Map<List<ProductDto>>(productsPerPage);
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto Sorting(string sort, int page = 1, int pageSize = 10)
        {
            try
            {
                List<Product> products;
                if (sort == "desc")
                {
                    products = _db.Product.Where(p => p.IsActive).OrderByDescending(x => x.Price).ToList();
                }
                else
                {
                    products = _db.Product.Where(p => p.IsActive).OrderBy(x => x.Price).ToList();
                }
                var totalCount = products.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = new
                {
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    Products = _mapper.Map<List<ProductDto>>(productsPerPage)
                };
                response.IsSuccess = true;
            }
            catch (Exception ex)
            {
                response.IsSuccess = false;
                response.Message = ex.Message;
            }
            return response;
        }

        public ResponseDto TopViewed(int page = 1, int pageSize = 10)
        {
            try
            {
                var products = _db.Product.Where(p => p.IsActive).OrderByDescending(x => x.View).ToList();
                var totalCount = products.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);
                var productsPerPage = products
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();
                response.Result = _mapper.Map<List<ProductDto>>(productsPerPage);
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
                response.Message = "Thức ăn đã cập nhật";
            }
            catch
            {
                response.IsSuccess = false;
                response.Message = $"Thức ăn này không tồn tại";
            }
            return response;
        }
    }
}
