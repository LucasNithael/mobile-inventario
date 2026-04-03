using inventario_api.Domain.Shared;
using inventario_api.DTOs;

namespace inventario_api.Services
{
    public interface IMovementService
    {
        Task<Result<ICollection<MovementOutput>>> GetAsync();
        Task<Result<ICollection<MovementOutput>>> GetByProductIdAsync(Guid productId);
        Task<Result<MovementOutput>> AddAsync(MovementInput input);
    }
}
