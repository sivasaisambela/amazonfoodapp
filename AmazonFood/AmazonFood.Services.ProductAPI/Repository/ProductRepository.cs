using AmazonFood.Services.ProductAPI.Data;
using AmazonFood.Services.ProductAPI.Models;
using AmazonFood.Services.ProductAPI.Models.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace AmazonFood.Services.ProductAPI.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ProductRepository(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product prd = _mapper.Map<ProductDto, Product>(productDto); 
            if(prd.ProductId>0)
            {
                _dbContext.Products.Update(prd);
               
            }
            else
            {
                _dbContext.Products.Add(prd);
                
            }
            await _dbContext.SaveChangesAsync();

            return _mapper.Map<Product, ProductDto>(prd);
        }

        public async Task<bool> DeleteProduct(int id)
        {
            try
            {
               Product p = await _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
               if (p != null) {
                    _dbContext.Products.Remove(p);
                    _dbContext.SaveChanges();
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        public async Task<ProductDto> GetProductById(int id)
        {
           Product prd= await _dbContext.Products.Where(x => x.ProductId == id).FirstOrDefaultAsync();
            return _mapper.Map<ProductDto>(prd);

        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            List<Product> prdList = await _dbContext.Products.ToListAsync();

            return _mapper.Map<List<ProductDto>>(prdList);
        }
    }
}
