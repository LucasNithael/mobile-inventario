using inventario_api.Data;
using inventario_api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace inventario_api.Repositories
{
    public class ProductRepository(AppDbContext context) : IProductRepository
    {
        private readonly AppDbContext _context = context;
        public async Task AddAsync(Product input)
        {
            await _context.Products.AddAsync(input);
        }

        public async Task DeleteAsync(Product product)
        {
             _context.Products.Remove(product);
        }

        public async Task<ICollection<Product>> GetAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Movements)
                .OrderBy(p => p.Name)
                .ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(Guid id)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Movements)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product input)
        {
            _context.Products.Update(input);
        }

        public async Task<bool> ExistsAsync(Guid id)
        {
            return await _context.Products.AnyAsync(p => p.Id == id);
        }

        public async Task<bool> ExistsAsync(string name, Guid id)
        {
            return await _context.Products
                .AnyAsync(p => p.Name.ToUpper() == name.ToUpper() && p.Id != id);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Products.AnyAsync(p => p.Name.ToUpper() == name.ToUpper());
        }
    }
}
