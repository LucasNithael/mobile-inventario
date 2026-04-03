using inventario_api.Domain.Entities;

namespace inventario_api.Repositories
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetAsync();
        Task<Product?> GetByIdAsync(Guid id);
        Task AddAsync(Product input);
        Task UpdateAsync(Product input);
        Task DeleteAsync(Product input);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string name);
        Task<bool> ExistsAsync(string name, Guid id);
    }
}
