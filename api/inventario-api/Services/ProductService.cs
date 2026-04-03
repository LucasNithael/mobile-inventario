using FluentValidation;
using inventario_api.Domain.Entities;
using inventario_api.Domain.Shared;
using inventario_api.DTOs;
using inventario_api.Repositories;

namespace inventario_api.Services
{
    public class ProductService(
        IProductRepository productRepository,
        ICategoryRepository categoryRepository,
        IValidator<ProductInput> validator,
        IUnitOfWork unitOfWork
    ) : IProductService
    {
        private readonly IProductRepository _productRepository = productRepository;
        private readonly ICategoryRepository _categoryRepository = categoryRepository;
        private readonly IValidator<ProductInput> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private static decimal CalculateStock(Product product)
        {
            if (product.Movements is null || !product.Movements.Any())
                return 0;

            var inbound = product.Movements
                .Where(m => m.Type == MovementType.INBOUND)
                .Sum(m => m.Quantity);

            var outbound = product.Movements
                .Where(m => m.Type == MovementType.OUTBOUND)
                .Sum(m => m.Quantity);

            return inbound - outbound;
        }

        private static int GetStatusCode(List<string> errors)
        {
            if (errors.Any(e => e.Contains("não encontrada") || e.Contains("não encontrado")))
                return 404;

            if (errors.Any(e => e.Contains("Já existe")))
                return 409;

            return 400;
        }

        public async Task<Result<ProductOutput>> AddAsync(ProductInput input)
        {
            var errors = new List<string>();

            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                errors.AddRange(validation.Errors.Select(e => e.ErrorMessage));

            var category = await _categoryRepository.GetByIdAsync(input.CategoryId);

            if (category is null)
                errors.Add("Categoria não encontrada.");

            if (await _productRepository.ExistsAsync(input.Name))
                errors.Add("Já existe um produto com esse nome.");

            if (errors.Any())
                return Result<ProductOutput>.Fail(errors, "Erro ao criar produto.", GetStatusCode(errors));

            var product = new Product
            {
                Name = input.Name.Trim(),
                MinimumQuantity = input.MinimumQuantity,
                CategoryId = category!.Id,
                Category = category
            };

            await _productRepository.AddAsync(product);
            await _unitOfWork.CommitAsync();

            var output = new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = category.Id,
                Category = category.Name,
                Quantity = CalculateStock(product),
                MinimumQuantity = product.MinimumQuantity,
                Created = product.Created
            };

            return Result<ProductOutput>.Created(output, "Produto criado com sucesso.");
        }

        public async Task<Result<ProductOutput>> DeleteAsync(Guid id)
        {
            var errors = new List<string>();

            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                errors.Add("Produto não encontrado.");

            if (errors.Any())
                return Result<ProductOutput>.Fail(errors, "Erro ao deletar produto.", GetStatusCode(errors));

            await _productRepository.DeleteAsync(product!);
            await _unitOfWork.CommitAsync();

            var output = new ProductOutput
            {
                Id = product!.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Category = product.Category.Name,
                Quantity = CalculateStock(product),
                MinimumQuantity = product.MinimumQuantity,
                Created = product.Created
            };

            return Result<ProductOutput>.Ok(output, "Produto deletado com sucesso.");
        }

        public async Task<Result<ICollection<ProductOutput>>> GetAsync()
        {
            var products = await _productRepository.GetAsync();

            var outputs = products.Select(product => new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = product.CategoryId,
                Category = product.Category.Name,
                Quantity = CalculateStock(product),
                MinimumQuantity = product.MinimumQuantity,
                Created = product.Created
            }).ToList();

            return Result<ICollection<ProductOutput>>
                .Ok(outputs, "Produtos recuperados com sucesso.");
        }

        public async Task<Result<ProductOutput>> UpdateAsync(Guid id, ProductInput input)
        {
            var errors = new List<string>();

            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                errors.AddRange(validation.Errors.Select(e => e.ErrorMessage));

            var product = await _productRepository.GetByIdAsync(id);

            if (product is null)
                errors.Add("Produto não encontrado.");

            var category = await _categoryRepository.GetByIdAsync(input.CategoryId);

            if (category is null)
                errors.Add("Categoria não encontrada.");

            if (await _productRepository.ExistsAsync(input.Name, id))
                errors.Add("Já existe outro produto com esse nome.");

            if (errors.Any())
                return Result<ProductOutput>.Fail(errors, "Erro ao atualizar produto.", GetStatusCode(errors));

            product!.Name = input.Name.Trim();
            product.CategoryId = category!.Id;
            product.Category = category;
            product.Updated = DateTime.UtcNow;
            product.MinimumQuantity = input.MinimumQuantity;

            await _productRepository.UpdateAsync(product);
            await _unitOfWork.CommitAsync();

            var output = new ProductOutput
            {
                Id = product.Id,
                Name = product.Name,
                CategoryId = category.Id,
                Category = category.Name,
                MinimumQuantity = product.MinimumQuantity,
                Quantity = CalculateStock(product),
                Created = product.Created
            };

            return Result<ProductOutput>.Ok(output, "Produto atualizado com sucesso.");
        }
    }
}