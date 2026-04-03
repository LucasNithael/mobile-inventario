using inventario_api.Data;
using inventario_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventario_api.Repositories
{
    public class MovementRepository(AppDbContext context) : IMovementRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAsync(Movement movement)
        {
            _context.Movements.Add(movement);
        }

        public async Task<ICollection<Movement>> GetAsync()
        {
            return await _context.Movements
                .Include(m => m.Product)
                .OrderByDescending(m => m.Created)
                .ToListAsync();
        }

        public async Task<ICollection<Movement>> GetByProductIdAsync(Guid productId)
        {
            return await _context.Movements
                .Where(m => m.ProductId == productId)
                .Include(m => m.Product)
                .OrderByDescending(m => m.Created)
                .ToListAsync();
        }
    }
}
