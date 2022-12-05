using AmazonFood.Web.Models;

namespace AmazonFood.Web.Services.IServices
{
    public interface IProductService : IBaseService
    {
        Task<T> GetAllProductsAsync<T>();

        Task<T> GetProductByIdAsync<T>(int id);

        Task<T> CreateProdcutAsync<T>(ProductDto prdcDto);

        Task<T> UpdateProdcutAsync<T>(ProductDto prdcDto);

        Task<T> DeleteProdcutAsync<T>(int id);

    }
}
