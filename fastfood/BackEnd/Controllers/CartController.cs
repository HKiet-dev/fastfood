using AutoMapper;
using Azure;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController(ICartService cartservice, IMapper mapper, UserManager<User> userManager) : ControllerBase
    {
        UserManager<User> _userManager = userManager;
        ICartService _cartservice = cartservice;
        IMapper _mapper = mapper;

        /// <summary>
        /// Tạo mới giỏ hàng cho khách hàng.
        /// </summary>
        /// <param name="dto">Thông tin mới của giỏ hàng.</param>
        /// <returns>Danh mục mới được tạo.</returns>
        /// <response code="201">Tạo giỏ hàng mới được tạo thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        [HttpPost("addtocart")]
        public ResponseDto Create([FromBody] CartDetailDto dto)
        {
            if (ModelState.IsValid)
            {
                var cart = _mapper.Map<CartDetail>(dto);
                cart.UserId = _userManager.GetUserId(User);
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
        /// Xóa sản phẩm theo Id người dùng
        /// </summary>
        /// <param name="id">Nhập Id của sản phẩm cần xóa.</param>
        /// <returns>Kết quả xóa giỏ hàng thành công</returns>
        /// <response code="200">Xóa giỏ hàng thành công.</response>
        /// <response code="404">Không tìm thấy giỏ hàng cần xóa.</response>
        [HttpDelete(("deletecart/{id:int}"))]
        public ResponseDto Delete([FromRoute] int id)
        {
            var userId = _userManager.GetUserId(User);
            var response = _cartservice.DeleteCart(id, userId);
            return response;
        }

        /// <summary>
        /// Tìm kiếm giỏ hàng theo id.
        /// </summary>
        /// <returns>Trả về danh sách giỏ hàng của khách hàng</returns>
        /// <response code="200">Trả về giỏ hàng  với sản phẩm, số lượng của khách hàng</response>
        /// <response code="404">Không tìm thấy không tìm thấy giỏ hàng nào của khách hàng</response>
        [HttpGet("find_id")]
        public ResponseDto GetById()
        {
            var userId = _userManager.GetUserId(User); ;
            return _cartservice.GetById(userId);
        }

        /// <summary>
        /// Xoá tất cả giỏ hàng của người dùng
        /// </summary>
        /// <returns>Trả về xoá tất cả danh sách giỏ hàng của người dùng</returns>
        /// <response code="200">Trả về giỏ hàng của người dùng đã được xoá</response>
        /// <response code="404">Không tìm thấy không tìm thấy giỏ hàng nào của khách hàng</response>
        [HttpDelete("delete-all-by-user-id")]
        public ResponseDto DeleteAllByUserId()
        {

            var userId = _userManager.GetUserId(User);
            var response = _cartservice.DeleteAllById(userId);
            return response; 
        }

    }
}
