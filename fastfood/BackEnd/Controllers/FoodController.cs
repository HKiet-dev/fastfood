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
    }
}
