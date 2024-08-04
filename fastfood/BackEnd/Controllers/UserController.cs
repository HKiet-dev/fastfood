using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        readonly IUserService _user;
        readonly IHelper _helper;
        readonly IMapper _mapper;
        public UserController(IUserService user, IHelper helper, IMapper mapper)
        {
            _user = user;
            _helper = helper;
            _mapper = mapper;
        }
        /// <summary>
        /// Lấy danh sách tất cả các tài khoản (có phân trang).
        /// </summary>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng tài khoản trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách các tài khoản.</returns>
        /// <response code="200">Trả về danh sách các tài khoản.</response>
        /// <response code="404">Không tìm thấy tài khoản nào.</response>
        [HttpGet]
        public async Task<ResponseDto> GetAll(int page = 1, int pageSize = 10)
        {
            var users = _user.GetAll(page, pageSize);
            return await users;
        }

        /// <summary>
        /// Tìm kiếm tài khoản bằng id.
        /// </summary>
        /// <param name="id">Id của tài khoản cần tìm</param>
        /// <returns>Tài khoản đã tìm thấy.</returns>
        /// <response code="200">Trả về 1 tài khoản đã tìm thấy.</response>
        /// <response code="404">Không tìm thấy tài khoản nào.</response>
        [HttpGet("{id}")]
        public async Task<ResponseDto> GetById(string id)
        {
            return await _user.GetById(id);
        }
        /// <summary>
        /// Tìm kiếm tài khoản bằng tên (có phân trang).
        /// </summary>
        /// <param name="query">Tên tài khoản cần tìm.</param>
        /// <param name="page">Trang hiện tại (mặc định là 1).</param>
        /// <param name="pageSize">Số lượng tài khoản trên mỗi trang (mặc định là 10).</param>
        /// <returns>Danh sách tài khoản giống tên nhau.</returns>
        /// <response code="200">Trả về danh sách tài khoản giống tên nhau.</response>
        /// <response code="404">Không tìm thấy tài khoản nào.</response>
        [HttpGet("search")]
        public async Task<ResponseDto> GetBySearch(string query, int page = 1, int pageSize = 10)
        {
            return await _user.GetBySearch(query, page, pageSize);
        }

        /// <summary>
        /// Tạo tài khoản mới.
        /// </summary>
        /// <param name="userDto">Thông tin của tài khoản mới.</param>
        /// <returns>Tài khoản mới được tạo.</returns>
        /// <response code="201">Tài khoản mới được tạo thành công và gửi email chứa mật khẩu tới tài khoản đã đăng kí.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        [HttpPost]
        public async Task<ResponseDto> Create([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDto);
                string randomPassword = _helper.GenerateRandomPassword();
                var response = await _user.Create(user, randomPassword);
                if (response.IsSuccess)
                    await _helper.SendMail(userDto.Email, "New password", randomPassword);
                return response;
            }
            return null;
        }

        /// <summary>
        /// Cập nhật thông tin tài khoản.
        /// </summary>
        /// <param name="userDto">Thông tin cập nhật của tài khoản.</param>
        /// <returns>Tài khoản sau khi được cập nhật.</returns>
        /// <response code="200">Tài khoản được cập nhật thành công.</response>
        /// <response code="400">Dữ liệu không hợp lệ hoặc thiếu thông tin bắt buộc.</response>
        /// <response code="404">Không tìm thấy tài khoản cần cập nhật.</response>
        [HttpPut]
        public async Task<ResponseDto> Update([FromBody] UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(userDto);
                var response = await _user.Update(user);
                return response;
            }
            return null;
        }
        /// <summary>
        /// Xóa tài khoản theo ID.
        /// </summary>
        /// <param name="id">ID của tài khoản cần xóa.</param>
        /// <returns>Kết quả xóa tài khoản.</returns>
        /// <response code="200">Xóa tài khoản thành công.</response>
        /// <response code="404">Không tìm thấy tài khoản cần xóa.</response>
        [HttpDelete("{id}")]
        public async Task<ResponseDto> Delete([FromRoute] string id)
        {
            if (ModelState.IsValid)
            {
                var response = await _user.Delete(id);
                return response;
            }
            return null;
        }
        
        [HttpPost("changepassword")]
        public async Task<ResponseDto> ChangePassword(ChangePassDto changepass)
        {
            if (ModelState.IsValid)
            {
                var response = await _user.ChangePassword(changepass);
                return response;
            }
            return null;
        }
    }
}
