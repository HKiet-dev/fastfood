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
                config.CreateMap<ProductDto, Product>().ReverseMap();
                config.CreateMap<CartDetailDto,CartDetail>().ReverseMap();
                config.CreateMap<OrderDto, Order>().ReverseMap();
                config.CreateMap<OrderDetailDto, OrderDetail>().ReverseMap();
                config.CreateMap<CartDetailDto, CartDetail>().ReverseMap();
            });
            return mappingconfig;
        }
    }
}
