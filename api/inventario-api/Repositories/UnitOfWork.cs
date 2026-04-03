using inventario_api.Data;

namespace inventario_api.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly AppDbContext _context = context;

        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
