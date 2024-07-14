using AutoMapper;
using BackEnd.Models;
using BackEnd.Models.Dtos;
using static Azure.Core.HttpHeader;

namespace BackEnd
{
    public class MapConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingconfig = new MapperConfiguration(config =>
            {
                // Viết Model muốn mapping với DTO vào đây 
                // VD : config.CreateMap<ProductDto, Products>().ReverseMap();
                config.CreateMap<CategoryDto, Category>();
                config.CreateMap<Category, CategoryDto>();
<<<<<<< HEAD
                config.CreateMap<UserDto, User>().ReverseMap();
=======
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
>>>>>>> 70f37cdb3eac1149b41d643ac00e7334cd56f1a7
            });
            return mappingconfig;
        }
    }
}
