using inventario_api.DTOs;
using inventario_api.Services;
using Microsoft.AspNetCore.Mvc;

namespace inventario_api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovementController(IMovementService movementService) : BaseController
    {
        private readonly IMovementService _movementService = movementService;

        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var result = await _movementService.GetAsync();
            return CustomResponse(result);
        }

        [HttpGet("product/{productId}")]
        public async Task<IActionResult> GetByProductIdAsync(Guid productId)
        {
            var result = await _movementService.GetByProductIdAsync(productId);
            return CustomResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] MovementInput input)
        {
            var result = await _movementService.AddAsync(input);
            return CustomResponse(result);
        }
    }
}