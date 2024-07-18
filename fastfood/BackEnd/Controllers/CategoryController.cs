using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Azure.Core.HttpHeader;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController(ICategoryService cateService, IMapper mapper) : ControllerBase
    {
        ICategoryService _cateService = cateService;
        IMapper _mapper = mapper;

        /// <summary>
        /// Lấy danh sách tất cả các danh mục.
        /// </summary>
        /// <returns>Danh sách các danh mục (Category).</returns>
        /// <response code="200">Trả về danh sách các danh mục.</response>
        /// <response code="404">Không tìm thấy danh mục nào.</response>
        [HttpGet]
        public ResponseDto GetAll()
        {
            return _cateService.GetAll();
        }

        /// <summary>
        /// Lấy danh mục theo ID.
        /// </summary>
        /// <param name="id">ID của danh mục cần lấy.</param>
        /// <returns>Danh mục có ID tương ứng.</returns>
        /// <response code="200">Trả về danh mục với ID đã chỉ định.</response>
        /// <response code="404">Không tìm thấy danh mục với ID đã chỉ định.</response>
        [HttpGet("{id:int}")]
        public ResponseDto GetById(int id)
        {
            return _cateService.GetById(id);
        }

        /// <summary>
        /// Tạo danh mục mới.
        /// </summary>
        /// <param name="categorydto">Thông tin của danh mục mới.</param>
        /// <returns>Danh mục mới được tạo.</returns>
        /// <response code="201">Danh mục mới được tạo thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        [HttpPost]
        public ResponseDto Create([FromBody] CategoryDto categorydto)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categorydto);
                var response = _cateService.Create(category);
                return response;
            }
            return null;
        }

        /// <summary>
        /// Cập nhật thông tin danh mục.
        /// </summary>
        /// <param name="category">Thông tin cập nhật của danh mục.</param>
        /// <returns>Danh mục sau khi được cập nhật.</returns>
        /// <response code="200">Danh mục được cập nhật thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc</response>
        /// <response code="404">Không tìm thấy danh mục cần cập nhật.</response>
        [HttpPut]
        public ResponseDto Update([FromBody] CategoryDto category)
        {
            if (ModelState.IsValid)
            {
                var response = _cateService.Update(_mapper.Map<Category>(category));
                return response;
            }
            return null;
        }

        /// <summary>
        /// Xóa danh mục theo ID.
        /// </summary>
        /// <param name="id">ID của danh mục cần xóa.</param>
        /// <returns>Kết quả xóa danh mục.</returns>
        /// <response code="200">Xóa danh mục thành công.</response>
        /// <response code="404">Không tìm thấy danh mục cần xóa.</response>
        [HttpDelete("{id:int}")]
        public ResponseDto Delete([FromRoute] int id)
        {
            var response = _cateService.Delete(id);
            return response;
        }

        /// <summary>
        /// Tìm kiếm danh mục theo tên.
        /// </summary>
        /// <param name="name">Tên danh mục cần tìm kiếm.</param>
        /// <returns>Danh sách các danh mục có tên khớp với từ khóa tìm kiếm.</returns>
        /// <response code="200">Trả về danh sách các danh mục khớp với tên.</response>
        /// <response code="404">Không tìm thấy danh mục nào khớp với tên.</response>
        [HttpGet("{name}")]
        public ResponseDto GetByName(string name)
        {
            var response = _cateService.GetByName(name);
            return response;
        }
    }
}
