using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ResponseDto _response;
        public AuthController(IAuthService authService) 
        {
            _authService = authService;
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
    }
}
