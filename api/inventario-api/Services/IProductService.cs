using inventario_api.Domain.Shared;
using inventario_api.DTOs;

namespace inventario_api.Services
{
    public interface IProductService
    {
        Task<Result<ICollection<ProductOutput>>> GetAsync();
        Task<Result<ProductOutput>> AddAsync(ProductInput input);
        Task<Result<ProductOutput>> UpdateAsync(Guid id, ProductInput input);
        Task<Result<ProductOutput>> DeleteAsync(Guid id);
    }
}
