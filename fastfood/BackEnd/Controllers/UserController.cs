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

        [HttpGet]
        public async Task<ResponseDto> GetAll(int page = 1, int pageSize = 10)
        {
            var products = _user.GetAll(page, pageSize);
            return await products;
        }


        [HttpGet("{id}")]
        public async Task<ResponseDto> GetById(string id)
        {
            return await _user.GetById(id);
        }
        [HttpGet("search/{query}")]
        public async Task<ResponseDto> GetBySearch(string query)
        {
            return await _user.GetBySearch(query);
        }


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
    }
}
