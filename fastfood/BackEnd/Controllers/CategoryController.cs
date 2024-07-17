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
        /// <returns >List of category</returns>
        /// <response code="200">Returns list of category</response>
        /// <response code="400">If list of category is null</response>
        [HttpGet]
        public ResponseDto GetAll()
        {
            return _cateService.GetAll();
        }

        /// <summary>
        /// Get category by id
        /// </summary>
        /// <returns>Category with id specify</returns>
        /// <response code="200">Returns category with specified id</response>
        /// <response code="400">If specified category id not found</response>
        [HttpGet("{id:int}")]
        public ResponseDto GetById(int id)
        {
            return _cateService.GetById(id);
        }

        /// <summary>
        /// Create new category
        /// </summary>
        /// <param name="categorydto"></param>
        /// <returns>A newly category created</returns>
        /// <response code="201">Newly category created</response>
        /// <response code="400">Null or invalid</response>
        [HttpPost]
        public ResponseDto Create([FromBody] CategoryDto categorydto)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<Category>(categorydto);
                var response = _cateService.Create(category);
                return response;
            }
            return null;
        }

        /// <summary>
        /// Update category
        /// </summary>
        /// <param name="category"></param>
        /// <returns>Category updated</returns>
        /// <response code="200">Category updated</response>
        /// <response code="400">This category is not exist</response>
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

        /// <summary>
        /// Delete category
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Category updated</returns>
        /// <response code="200">Category updated</response>
        /// <response code="400">Null or invalid</response>
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
