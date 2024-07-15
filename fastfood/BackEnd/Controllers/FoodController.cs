using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using BackEnd.Repository.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController(IFoodService foodService, IMapper mapper) : ControllerBase
    {
        readonly IFoodService _foodService = foodService;
        readonly IMapper _mapper = mapper;

        [HttpGet]
        public ResponseDto GetAll(int page = 1, int pageSize = 10)
        {
            var products = _foodService.GetAll(page,pageSize);
            return products;
        }

        [HttpGet("{id:int}")]
        public ResponseDto GetById(int id)
        {
            return _foodService.GetById(id);
        }

        [HttpPost]
        public ResponseDto Create([FromBody] ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(productDto);
                var response = _foodService.Create(product);
                return response;
            }
            return null;
        }

        [HttpDelete("{id:int}")]
        public ResponseDto Delete([FromRoute]int id)
        {
            return _foodService.Delete(id);
        }

        [HttpGet("{categoryId:int}")]
        public ResponseDto GetByCategoryId([FromRoute] int categoryId, int page = 1, int pageSize = 10)
        {
            var products = _foodService.GetByCategoryId(categoryId, page, pageSize);
            return products;
        }

        [HttpGet("Filter")]
        public ResponseDto GetByFilter(int? categoryid, decimal? price, int page = 1, int pageSize = 10)
        {
            return _foodService.GetByFilter(categoryid, price, page, pageSize);
        }

        [HttpGet("Get By Search")]
        public ResponseDto GetBySearch(string query, int page = 1, int pageSize = 10)
        {
            return _foodService.GetBySearch(query, page, pageSize);
        }

        [HttpGet("Get top view")]
        public ResponseDto TopViewed(int page = 1, int pageSize = 10)
        {
            return _foodService.TopViewed(page, pageSize);
        }

        [HttpPut]
        public ResponseDto Update([FromBody]ProductDto productDto)
        {
            if (ModelState.IsValid)
            {
                var response = _foodService.Update(_mapper.Map<Product>(productDto));
                return response;
            }
            return null;
        }
    }
}
