using AmazonFood.Web.Models;
using AmazonFood.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AmazonFood.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if (ModelState.IsValid)
            {


                var response = await _productService.CreateProdcutAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                   return RedirectToAction(nameof(ProductIndex));
                }
            }
           
                return View(model);
            
        }

        [HttpGet]
        public async Task<IActionResult> ProductEdit(int productId)
        {
            
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductUpdate(ProductDto model)
        {
            if (ModelState.IsValid)
            {


                var response = await _productService.UpdateProdcutAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }

            return View(model);

        }


        
        public async Task<IActionResult> ProductDelete(int productId)
        {

            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            


                var response = await _productService.DeleteProdcutAsync<ResponseDto>(model.ProductId);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            return View(model);

            

        }

    }
}
