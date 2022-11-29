using AmazonFood.Services.ProductAPI.Models.DTO;
using AmazonFood.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace AmazonFood.Services.ProductAPI.Controllers
{
    [Route("api/products")]
    public class ProductAPIController : ControllerBase
    {
        private readonly ResponseDto _responseDto;
        private readonly IProductRepository _productRepository;

        public ProductAPIController(ResponseDto responseDto,IProductRepository productRepository)
        {
            _responseDto = responseDto;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ResponseDto> Get()
        {
            try
            {
                IEnumerable<ProductDto> productDtos= await  _productRepository.GetProducts();
                _responseDto.Result = productDtos;
            }
            catch(Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ResponseDto> Get(int Id)
        {
            try
            {
                ProductDto productDto = await _productRepository.GetProductById(Id);
                _responseDto.Result = productDto;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }


        [HttpPost]
       
        public async Task<ResponseDto> Post([FromBody] ProductDto productDto)
        {
            try
            {
             ProductDto p=  await _productRepository.CreateUpdateProduct(productDto);
                _responseDto.Result = p;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }

        [HttpPut]

        public async Task<ResponseDto> Put([FromBody] ProductDto productDto)
        {
            try
            {
                ProductDto p = await _productRepository.CreateUpdateProduct(productDto);
                _responseDto.Result = p;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

            return _responseDto;
        }


        [HttpDelete]

        public async Task<ResponseDto> Delete(int ProductId)
        {

            try
            {
                 bool opt= await _productRepository.DeleteProduct(ProductId);
                _responseDto.Result = opt;
            }
            catch (Exception ex)
            {
                _responseDto.IsSuccess = false;
                _responseDto.ErrorMessage = new List<string>() { ex.ToString() };
            }

             return _responseDto;
        }
    }
}
