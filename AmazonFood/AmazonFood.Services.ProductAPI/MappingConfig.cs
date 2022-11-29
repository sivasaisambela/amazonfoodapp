using AmazonFood.Services.ProductAPI.Models;
using AmazonFood.Services.ProductAPI.Models.DTO;
using AutoMapper;

namespace AmazonFood.Services.ProductAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(
                config =>
                {
                    config.CreateMap<ProductDto, Product>();
                    config.CreateMap<Product, ProductDto>();
                }

            );
            return mappingConfig;
        }
    }
}
