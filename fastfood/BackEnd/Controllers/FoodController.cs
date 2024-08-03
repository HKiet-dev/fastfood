using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController(IFoodService foodService, IMapper mapper) : ControllerBase
    {
        readonly IFoodService _foodService = foodService;
        readonly IMapper _mapper = mapper;

        /// <summary>
        /// Lấy danh sách tất cả các sản phẩm (có phân trang).
        /// </summary>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các sản phẩm.</returns>
        /// <response code="200">Trả về danh sách các sản phẩm.</response>
        /// <response code="404">Không tìm thấy sản phẩm nào.</response>
        [HttpGet]
        public ActionResult<ResponseDto> GetAll(int page = 1, int pageSize = 10)
        {
            var response = _foodService.GetAll(page, pageSize);

            if (!response.IsSuccess)
            {
                return NotFound(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Lấy sản phẩm theo ID.
        /// </summary>
        /// <param name="id">ID của sản phẩm cần lấy.</param>
        /// <returns>Sản phẩm có ID tương ứng.</returns>
        /// <response code="200">Trả về sản phẩm với ID đã chỉ định.</response>
        /// <response code="404">Không tìm thấy sản phẩm với ID đã chỉ định.</response>
        [HttpGet("product/{id:int}")]
        public ResponseDto GetById(int id)
        {
            return _foodService.GetById(id);
        }

        /// <summary>
        /// Tạo sản phẩm mới.
        /// </summary>
        /// <param name="productDto">Thông tin của sản phẩm mới.</param>
        /// <returns>Sản phẩm mới được tạo.</returns>
        /// <response code="201">Sản phẩm mới được tạo thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        [HttpPost]
        public ResponseDto Create([FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                var response = _foodService.Create(product);
                return response;
            }
            return null;
        }

        /// <summary>
        /// Xóa sản phẩm theo ID.
        /// </summary>
        /// <param name="id">ID của sản phẩm cần xóa.</param>
        /// <returns>Kết quả xóa sản phẩm.</returns>
        /// <response code="200">Xóa sản phẩm thành công.</response>
        /// <response code="404">Không tìm thấy sản phẩm cần xóa.</response>
        [HttpDelete("{id:int}")]
        public ResponseDto Delete([FromRoute]int id)
        {
            return _foodService.Delete(id);
        }

        /// <summary>
        /// Lấy danh sách sản phẩm theo ID của danh mục (có phân trang).
        /// </summary>
        /// <param name="categoryId">ID của danh mục.</param>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các sản phẩm thuộc danh mục đã chỉ định.</returns>
        /// <response code="200">Trả về danh sách các sản phẩm thuộc danh mục.</response>
        /// <response code="404">Không tìm thấy sản phẩm nào thuộc danh mục.</response>
        [HttpGet("{categoryId:int}")]
        public ResponseDto GetByCategoryId([FromRoute] int categoryId, int page = 1, int pageSize = 10)
        {
            var products = _foodService.GetByCategoryId(categoryId, page, pageSize);
            return products;
        }

        /// <summary>
        /// Lọc sản phẩm theo danh mục và giá (có phân trang).
        /// </summary>
        /// <param name="minrange">Giá thấp nhất.</param>
        /// <param name="maxrange">Giá cao nhất.</param>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các sản phẩm phù hợp với bộ lọc.</returns>
        /// <response code="200">Trả về danh sách các sản phẩm phù hợp với bộ lọc.</response>
        /// <response code="404">Không tìm thấy sản phẩm nào phù hợp với bộ lọc.</response>
        [HttpGet("Filter")]
        public ResponseDto GetByFilter(decimal? minrange, decimal? maxrange, int page = 1, int pageSize = 10)
        {
            return _foodService.GetByFilter(minrange, maxrange, page, pageSize);
        }

        /// <summary>
        /// Tìm kiếm sản phẩm theo từ khóa (có phân trang).
        /// </summary>
        /// <param name="query">Từ khóa tìm kiếm.</param>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các sản phẩm phù hợp với từ khóa tìm kiếm.</returns>
        /// <response code="200">Trả về danh sách các sản phẩm phù hợp với từ khóa tìm kiếm.</response>
        /// <response code="404">Không tìm thấy sản phẩm nào phù hợp với từ khóa tìm kiếm.</response>
        [HttpGet("Get-by-search")]
        public ResponseDto GetBySearch(string query, int page = 1, int pageSize = 10)
        {
            return _foodService.GetBySearch(query, page, pageSize);
        }

        /// <summary>
        /// Lọc sản phẩm theo giá (có phân trang).
        /// </summary>
        /// <param name="sort">Từ khóa lọc.</param>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các sản phẩm theo giá tăng giảm dần.</returns>
        /// <response code="200">Danh sách các sản phẩm theo giá tăng giảm dần.</response>
        /// <response code="404">Không tìm thấy sản phẩm nào.</response>
        [HttpGet("Sorting")]
        public ResponseDto Sorting(string sort, int page = 1, int pageSize = 10)
        {
            return _foodService.Sorting(sort, page, pageSize);
        }

        /// <summary>
        /// Lấy danh sách sản phẩm được xem nhiều nhất (có phân trang).
        /// </summary>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng sản phẩm trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các sản phẩm được xem nhiều nhất.</returns>
        /// <response code="200">Trả về danh sách các sản phẩm được xem nhiều nhất.</response>
        /// <response code="404">Không tìm thấy sản phẩm nào.</response>
        [HttpGet("Get-top-view")]
        public ResponseDto TopViewed(int page = 1, int pageSize = 10)
        {
            return _foodService.TopViewed(page, pageSize);
        }

        /// <summary>
        /// Cập nhật thông tin sản phẩm.
        /// </summary>
        /// <param name="productDto">Thông tin cập nhật của sản phẩm.</param>
        /// <returns>Sản phẩm sau khi được cập nhật.</returns>
        /// <response code="200">Sản phẩm được cập nhật thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        /// <response code="404">Không tìm thấy sản phẩm cần cập nhật.</response>
        [HttpPut]
        public ResponseDto Update([FromBody]ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = _foodService.Update(_mapper.Map<Product>(productDto));
                return response;
            }
            return null;
        }
    }
}
