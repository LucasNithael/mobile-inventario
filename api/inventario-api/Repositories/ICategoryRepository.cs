using inventario_api.Domain.Entities;

namespace inventario_api.Repositories
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetAsync();
        Task<Category?> GetByIdAsync(Guid id);
        Task AddAsync(Category input);
        Task UpdateAsync(Category input);
        Task DeleteAsync(Category input);
        Task<bool> ExistsAsync(Guid id);
        Task<bool> ExistsAsync(string id);
        Task<bool> ExistsAsync(string name, Guid id);
    }
}
