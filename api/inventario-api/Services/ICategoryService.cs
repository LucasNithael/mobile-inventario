using inventario_api.Domain.Entities;
using inventario_api.Domain.Shared;
using inventario_api.DTOs;

namespace inventario_api.Services
{
    public interface ICategoryService
    {
        Task<Result<ICollection<CategoryOutput>>> GetAsync();
        Task<Result<CategoryOutput>> AddAsync(CategoryInput input);
        Task<Result<CategoryOutput>> UpdateAsync(Guid id, CategoryInput input);
        Task<Result<CategoryOutput>> DeleteAsync(Guid id);
    }
}
