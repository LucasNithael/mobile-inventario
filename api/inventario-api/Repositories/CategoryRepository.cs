using inventario_api.Data;
using inventario_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventario_api.Repositories
{
    public class CategoryRepository(AppDbContext context) : ICategoryRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAsync(Category input)
        {
            await _context.Categories.AddAsync(input);
        }

        public async Task DeleteAsync(Category input)
        {
            _context.Categories.Remove(input);
        }

        public async Task<ICollection<Category>> GetAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task UpdateAsync(Category input)
        {
            _context.Categories.Update(input);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Categories.AnyAsync(c => c.Name.ToUpper() == name.ToUpper());
        }

        public async Task<bool> ExistsAsync(string name, Guid id)
        {
            return await _context.Categories
                .AnyAsync(p => p.Name.ToUpper() == name.ToUpper() && p.Id != id);
        }

        public async Task<Category?> GetByIdAsync(Guid id)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id)
                .Include(c => c.Products)
                .FirstOrDefaultAsync();

            return category;
        }

    }
}
