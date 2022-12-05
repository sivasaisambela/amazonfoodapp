using AmazonFood.Web.Models;
using AmazonFood.Web.Services.IServices;

namespace AmazonFood.Web.Services
{
    public class ProductService : BaseService, IProductService
    {
        private readonly IHttpClientFactory clientFactory;

        public ProductService(IHttpClientFactory clientFactory) : base(clientFactory) 
        {
            this.clientFactory = clientFactory;
        }
        public async Task<T> CreateProdcutAsync<T>(ProductDto prdcDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = prdcDto,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> DeleteProdcutAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
               
                Url = SD.ProductAPIBase + "/api/products/"+ id,
                AccessToken = ""
            });
        }

        public async Task<T> GetAllProductsAsync<T>()
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }

        public async Task<T> GetProductByIdAsync<T>(int id)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,

                Url = SD.ProductAPIBase + "/api/products/"+id,
                AccessToken = ""
            });
        }

        public async Task<T> UpdateProdcutAsync<T>(ProductDto prdcDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = prdcDto,
                Url = SD.ProductAPIBase + "/api/products",
                AccessToken = ""
            });
        }
    }
}
