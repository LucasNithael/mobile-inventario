using inventario_api.Domain.Entities;

namespace inventario_api.Repositories
{
    public interface IMovementRepository
    {
        Task<ICollection<Movement>> GetAsync();
        Task AddAsync(Movement movement);
        Task<ICollection<Movement>> GetByProductIdAsync(Guid productId);
    }
}
