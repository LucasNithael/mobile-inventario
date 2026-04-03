using System.Net;
using inventario_api.DTOs;
using inventario_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventario_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : BaseController
    {
        private readonly IProductService _productService = productService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _productService.GetAsync();
            return CustomResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] ProductInput input)
        {
            var result = await _productService.AddAsync(input);
            return CustomResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, ProductInput input)
        {
            var result = await _productService.UpdateAsync(id, input);
            return CustomResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _productService.DeleteAsync(id);
            return CustomResponse(result);
        }

    }
}
