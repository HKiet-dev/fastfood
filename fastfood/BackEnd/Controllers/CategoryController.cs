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
        /// Get all category
        /// </summary>
        /// <returns>List of category</returns>
        [HttpGet]
        public ResponseDto GetAll()
        {
            return _cateService.GetAll();
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns code="200">Category with id</returns>
        /// <returns code="400">Category id is not existed</returns>
        [HttpGet("{id:int}")]
        public ResponseDto GetById(int id)
        {
            return _cateService.GetById(id);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
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
    }
}
