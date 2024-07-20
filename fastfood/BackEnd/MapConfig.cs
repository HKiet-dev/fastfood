﻿using AutoMapper;
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
                config.CreateMap<CategoryDto, Category>().ReverseMap();
                config.CreateMap<UserDto, User>().ReverseMap();
<<<<<<< HEAD
                config.CreateMap<ProductDto, Product>();
                config.CreateMap<Product, ProductDto>();
                config.CreateMap<UserDto, User>();
                config.CreateMap<User, UserDto>();
=======
                config.CreateMap<ProductDto, Product>().ReverseMap();
>>>>>>> main
            });
            return mappingconfig;
        }
    }
}
