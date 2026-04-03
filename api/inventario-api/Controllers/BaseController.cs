using inventario_api.Domain.Shared;
using Microsoft.AspNetCore.Mvc;

namespace inventario_api.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult CustomResponse(Result result)
        {
            if (result.StatusCode == 204)
                return NoContent();

            return StatusCode(result.StatusCode, new
            {
                success = result.Success,
                message = result.Message,
                errors = result.Errors.Any() ? result.Errors : null
            });
        }

        protected IActionResult CustomResponse<T>(Result<T> result)
        {
            if (result.StatusCode == 204)
                return NoContent();

            return StatusCode(result.StatusCode, new
            {
                success = result.Success,
                message = result.Message,
                data = result.Data,
                errors = result.Errors.Any() ? result.Errors : null
            });
        }
    }
}