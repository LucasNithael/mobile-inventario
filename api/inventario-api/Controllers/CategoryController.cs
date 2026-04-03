using inventario_api.DTOs;
using inventario_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventario_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController(ICategoryService categoryService) : BaseController
    {
        private readonly ICategoryService _categoryService= categoryService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _categoryService.GetAsync();
            return CustomResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CategoryInput input)
        {
            var result = await _categoryService.AddAsync(input);
            return CustomResponse(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, CategoryInput input)
        {
            var result = await _categoryService.UpdateAsync(id, input);
            return CustomResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return CustomResponse(result);
        }
    }
}