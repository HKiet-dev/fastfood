using AutoMapper;
using Azure;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartservice , IMapper mapper) : ControllerBase
    {
        ICartService _cartservice = cartservice;
        IMapper _mapper = mapper;

        /// <summary>
        /// Tạo mới giỏ hàng cho khách hàng.
        /// </summary>
        /// <param name="cartdetailsDto">Thông tin mới của giỏ hàng.</param>
        /// <returns>Danh mục mới được tạo.</returns>
        /// <response code="201">Tạo giỏ hàng mới được tạo thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        [HttpPost("addcart")]
        public ResponseDto Create([FromBody] CartDetailDto dto)
        {
            if (ModelState.IsValid)
            {
                var cart = _mapper.Map<CartDetail>(dto);
                var response = _cartservice.CreateCart(cart); 
                return response;
            }
            return null;
        }

        /// <summary>
        /// Lấy danh sách tất cả các danh sách giỏ hàng của khách hàng.
        /// </summary>
        /// <returns>Danh sách các danh mục (CartDetails).</returns>
        /// <response code="200">Trả về danh sách các giỏ hàng của khách hàng.</response>
        /// <response code="404">Không tìm thấy giỏ hàng nào.</response>
        [HttpGet("cartall")]
        public ResponseDto GetAll()
        {
            return _cartservice.GetAll();
        }

        /// <summary>
        /// Xóa danh mục theo UserId và ProductId.
        /// </summary>
        /// <param name="id">ID của danh mục cần xóa.</param>
        /// <returns>Kết quả xóa giỏ hàng thành công</returns>
        /// <response code="200">Xóa giỏ hàng thành công.</response>
        /// <response code="404">Không tìm thấy giỏ hàng cần xóa.</response>
        [HttpDelete(("deletecart{id:int}"))]
        public ResponseDto Delete([FromRoute]int id, string userId)
        {
            var response = _cartservice.DeleteCart(id, userId);
            return response;
        }

        /// <summary>
        /// Tìm kiếm giỏ hàng theo id.
        /// </summary>
        /// <param name="userId">Id người dùng cần tìm kiếm giỏ hàng.</param>
        /// <returns>Trả về danh sách giỏ hàng của khách hàng</returns>
        /// <response code="200">Trả về giỏ hàng  với sản phẩm, số lượng của khách hàng</response>
        /// <response code="404">Không tìm thấy không tìm thấy giỏ hàng nào của khách hàng</response>
        [HttpGet("find_id")]
        public ResponseDto GetById(string userId)
        {
            return _cartservice.GetById(userId);
        }
    }
}
