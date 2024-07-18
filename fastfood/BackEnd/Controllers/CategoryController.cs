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


        [HttpGet]
        public ResponseDto GetAll()
        {
            return _cateService.GetAll();
        }


        [HttpGet("{id:int}")]
        public ResponseDto GetById(int id)
        {
            return _cateService.GetById(id);
        }


        [HttpPost]
        public ResponseDto Create([FromBody] CategoryDto dto)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(dto);
                var response = _cateService.Create(category);
                return response;
            }
            return null;
        }

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

        [HttpDelete("{id:int}")]
        public ResponseDto Delete([FromRoute] int id)
        {
            var response = _cateService.Delete(id);
            return response;
        }

        [HttpGet("{name}")]
        public ResponseDto GetByName(string name)
        {
            var response = _cateService.GetByName(name);
            return response;
        }
    }
}
