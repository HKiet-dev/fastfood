using AutoMapper;

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
            });
            return mappingconfig;
        }
    }
}
