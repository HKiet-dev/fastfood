﻿using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using BackEnd.Repository.Services;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace BackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private const string AuthError = "Xảy ra lỗi trong quá trình xác thực người dùng";
        private readonly IAuthService _authService;
        private readonly IMapper _mapper;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        protected ResponseDto _response;
        public AuthController(IAuthService authService, IMapper mapper, IJwtTokenGenerator jwtTokenGenerator) 
        {
            _authService = authService;
            _jwtTokenGenerator = jwtTokenGenerator;
            _mapper = mapper;
            _response = new ResponseDto();
        }

        /// <summary>
        /// Đăng ký người dùng mới.
        /// </summary>
        /// <param name="obj">Thông tin tài khoản đăng ký.</param>
        /// <returns>Kết quả đăng ký.</returns>
        /// <response code="200">Đăng ký thành công.</response>
        /// <response code="400">Đăng ký thất bại (có thể do email đã tồn tại hoặc dữ liệu không hợp lệ).</response>
        [HttpPost("auth")]
        public async Task<IActionResult> Register([FromBody] RegistrationRequestDto obj)
        {
            var errorMessage = await _authService.Resgister(obj);
            if (!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Đăng nhập.
        /// </summary>
        /// <param name="model">Thông tin đăng nhập.</param>
        /// <returns>Thông tin người dùng nếu đăng nhập thành công.</returns>
        /// <response code="200">Đăng nhập thành công.</response>
        /// <response code="400">Đăng nhập thất bại (tài khoản hoặc mật khẩu không đúng).</response>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto model)
        {
            var loginResponse = await _authService.Login(model);
            if (loginResponse.User == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Tài khoản hoặc mật khẩu không đúng";
                return BadRequest(_response);
            }
            _response.Result = loginResponse;
            return Ok(_response);
        }

        /// <summary>
        /// Gán vai trò cho người dùng.
        /// </summary>
        /// <param name="model">Thông tin người dùng và vai trò.</param>
        /// <returns>Kết quả gán vai trò.</returns>
        /// <response code="200">Gán vai trò thành công.</response>
        /// <response code="400">Gán vai trò thất bại.</response>
        [HttpPost("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegistrationRequestDto model)
        {
            var assignRoleSuccessful = await _authService.AssignRole(model.Email, model.Role.ToUpper());
            if (!assignRoleSuccessful)
            {
                _response.IsSuccess = false;
                _response.Message = "Xảy ra lỗi trong quá trình xác thực vai trò người dùng";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Lấy ID người dùng dựa trên email.
        /// </summary>
        /// <param name="email">Email của người dùng.</param>
        /// <returns>ID của người dùng.</returns>
        /// <response code="200">Trả về ID người dùng.</response>
        /// <response code="400">Không tìm thấy người dùng.</response>
        [HttpGet("userid")]
        public async Task<IActionResult> GetUserId(string email)
        {
            var getrole = await _authService.GetUserId(email);
            if (getrole == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Không tìm thấy người dùng";
                return BadRequest(_response);
            }
            return Ok(_response);
        }

        /// <summary>
        /// Lấy vai trò của người dùng.
        /// </summary>
        /// <param name="user">Thông tin người dùng.</param>
        /// <returns>Vai trò của người dùng.</returns>
        /// <response code="200">Trả về vai trò của người dùng.</response>
        /// <response code="400">Không xác định được vai trò người dùng.</response>
        [HttpGet("userrole")]
        public async Task<IActionResult> GetUserRole(UserDto user)
        {
            var getrole = await _authService.GetUserRole(_mapper.Map<User>(user));
            if (getrole == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Không xác định được vai trò người dùng";
                return BadRequest(_response);
            }
            _response.Result = getrole;
            return Ok(_response);
        }

        /// <summary>
        /// Lấy thông tin người dùng dựa trên email.
        /// </summary>
        /// <param name="email">Email của người dùng.</param>
        /// <returns>Thông tin người dùng.</returns>
        /// <response code="200">Trả về thông tin người dùng.</response>
        /// <response code="400">Người dùng không tồn tại.</response>
        [HttpGet("useremail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var getUser = await _authService.GetUserByEmail(email);
            if (getUser == null)
            {
                _response.IsSuccess = false;
                _response.Message = "Người dùng không tồn tại";
                return BadRequest(_response);
            }
            _response.Result = getUser;
            return Ok(_response);
        }

        /// <summary>
        /// Tạo người dùng mới từ thông tin đăng nhập Google.
        /// </summary>
        /// <param name="email">Email của người dùng.</param>
        /// <param name="name">Tên của người dùng.</param>
        /// <returns>Thông tin người dùng mới được tạo.</returns>
        /// <response code="200">Tạo người dùng thành công.</response>
        /// <response code="400">Tạo người dùng thất bại.</response>
        [HttpPost("GoogleAccount")]
        public async Task<IActionResult> CreateUserFromGoogleLogin(string email, string name)
        {
            var result = await _authService.CreateUserFromGoogleLogin(email, name);
            if (result == null)
            {
                _response.IsSuccess = false;
                _response.Message = AuthError;
                return BadRequest(_response);
            }
            _response.Result = result;
            return Ok(_response);
        }

        /// <summary>
        /// Lấy thông tin người dùng dựa trên ID.
        /// </summary>
        /// <param name="userId">ID của người dùng.</param>
        /// <returns>Thông tin người dùng.</returns>
        /// <response code="200">Trả về thông tin người dùng.</response>
        /// <response code="400">Lỗi xảy ra trong quá trình xác thực người dùng.</response>
        [HttpGet("userbyid/{userId}")]
        public async Task<IActionResult> GetUserById([FromRoute] string userId)
        {
            var result = await _authService.GetUserById(userId);
            if (result == null)
            {
                _response.IsSuccess = false;
                _response.Message = AuthError;
                return BadRequest(_response);
            }
            _response.Result = result;
            return Ok(_response);
        }

        /// <summary>
        /// Tạo token JWT cho người dùng.
        /// </summary>
        /// <param name="user">Thông tin người dùng.</param>
        /// <returns>Token JWT.</returns>
        /// <response code="200">Trả về token JWT.</response>
        /// <response code="400">Lỗi xảy ra trong quá trình xác thực người dùng.</response>
        [HttpGet("jwtauth")]
        public async Task<IActionResult> JWTGenerator(UserDto user)
        {
            var result = await _authService.GenerateJwt(user);
            if (result == null)
            {
                _response.IsSuccess = false;
                _response.Message = AuthError;
                return BadRequest(_response);
            }
            _response.Result = result;
            return Ok(_response);
        }

        /// <summary>
        /// Bắt đầu quá trình đăng nhập bằng Google.
        /// </summary>
        /// <returns>Chuyển hướng đến trang đăng nhập của Google.</returns>
        [HttpGet("signin-google")]
        public async Task<IActionResult> ExternalLoginGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("ExternalLoginCallback") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

        /// <summary>
        /// Xử lý callback sau khi đăng nhập bằng Google.
        /// </summary>
        /// <returns>Chuyển hướng về frontend với token JWT (nếu thành công đăng nhập thành công).</returns>
        [HttpGet("external-login-callback")]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            var authenticationResult = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);
            if (!authenticationResult.Succeeded)
            {
                return null;
            }

            var claims = authenticationResult.Principal.Claims;


            var email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var name = claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            if (email == null)
            {
                return BadRequest(AuthError);
            }

            var user = await _authService.CreateUserFromGoogleLogin(email, name);
            if (user == null)
            {
                return BadRequest(AuthError);
            }
            var role = await _authService.GetUserRole(user);
            // Generate JWT token and return
            /*var token = await _authService.GenerateJwt(user);*/
            var token = _jwtTokenGenerator.GenerateToken(user, role);

            LoginResponseDto? result = new()
            {
                User = _mapper.Map<UserDto>(user),
                Token = token,
                Role = role
            };

            var jsonResponse = JsonConvert.SerializeObject(result);
            var base64Response = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(jsonResponse));

            // Redirect to the MVC front-end with the token in the URL or via POST form
            var redirectUrl = $"https://localhost:7192/Auth/GoogleLoginCallback?data={base64Response}";

            return Redirect(redirectUrl);
        }
    }
}
