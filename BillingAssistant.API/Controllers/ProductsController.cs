using BillingAssistant.Business.Abstract;
using BillingAssistant.Entities.DTOs.ProductDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace BillingAssistant.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> AddProductFromOCR(IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File is empty");
            }
    
            var result = await _productService.AddProductFromOCR(file);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> GetProducts()
        {
            var result = await _productService.GetListAsync();
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpGet]
        [Route("[action]/{productId:int}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var result = await _productService.GetByIdAsync(productId);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> CreateProduct(ProductAddDto productAddDto)
        {
            var result = await _productService.AddAsync(productAddDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
        [HttpDelete]
        [Route("[action]/{productId:int}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteAsync(productId);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest();
        }
        [HttpPut]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var result = await _productService.UpdateAsync(productUpdateDto);
            if (result != null)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}