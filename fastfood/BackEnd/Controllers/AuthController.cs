using BackEnd.Models;
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

        [HttpPost("GoogleAccount")]
        public async Task<IActionResult> CreateUserFromGoogleLogin(UserDto user)
        {
            var result = await _authService.CreateUserFromGoogleLogin(user);
            if (result == null)
            {
                _response.IsSuccess = false;
                _response.Message = AuthError;
                return BadRequest(_response);
            }
            _response.Result = result;
            return Ok(_response);
        }

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

        [HttpGet("signin-google")]
        public async Task<IActionResult> ExternalLoginGoogle()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("ExternalLoginCallback") };
            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }

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
            var phone = claims.FirstOrDefault(x => x.Type == ClaimTypes.MobilePhone)?.Value;
            var address = claims.FirstOrDefault(x => x.Type == ClaimTypes.StreetAddress)?.Value;

            if (email == null)
            {
                return BadRequest(AuthError);
            }

            var user = new UserDto
            {
                Email = email,
                Name = name,
                Phone = phone,
                Address = address
            };

            var action = await _authService.CreateUserFromGoogleLogin(user);

            if (action == null)
            {
                return BadRequest(AuthError);
            }
            var role = await _authService.GetUserRole(action);
            // Generate JWT token and return
            /*var token = await _authService.GenerateJwt(user);*/
            var token = _jwtTokenGenerator.GenerateToken(action, role);

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
