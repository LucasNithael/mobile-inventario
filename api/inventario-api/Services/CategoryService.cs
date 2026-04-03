using FluentValidation;
using inventario_api.Domain.Entities;
using inventario_api.Domain.Shared;
using inventario_api.DTOs;
using inventario_api.Repositories;

namespace inventario_api.Services
{
    public class CategoryService(
        ICategoryRepository repository,
        IValidator<CategoryInput> validator,
        IUnitOfWork unitOfWork
    ) : ICategoryService
    {
        private readonly ICategoryRepository _repository = repository;
        private readonly IValidator<CategoryInput> _validator = validator;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        private static int GetStatusCode(List<string> errors)
        {
            if (errors.Any(e => e.Contains("não encontrada") || e.Contains("não encontrado")))
                return 404;

            if (errors.Any(e => e.Contains("Já existe")))
                return 409;

            return 400;
        }

        public async Task<Result<CategoryOutput>> AddAsync(CategoryInput input)
        {
            var errors = new List<string>();

            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                errors.AddRange(validation.Errors.Select(e => e.ErrorMessage));

            if (await _repository.ExistsAsync(input.Name))
                errors.Add("Já existe uma categoria com esse nome.");

            if (errors.Any())
                return Result<CategoryOutput>.Fail(errors, "Erro ao criar categoria.", GetStatusCode(errors));

            var category = new Category
            {
                Name = input.Name.Trim()
            };

            await _repository.AddAsync(category);
            await _unitOfWork.CommitAsync();

            var output = new CategoryOutput
            {
                Id = category.Id,
                Name = category.Name,
                ProductQuantity = 0,
                Created = category.Created
            };

            return Result<CategoryOutput>.Created(output, "Categoria criada com sucesso.");
        }

        public async Task<Result<CategoryOutput>> DeleteAsync(Guid id)
        {
            var errors = new List<string>();

            var category = await _repository.GetByIdAsync(id);

            if (category is null)
                errors.Add("Categoria não encontrada.");

            if (category is not null && category.Products.Any())
            {
                var count = category.Products.Count;
                errors.Add(
                    count == 1 ?
                    $"Há 1 produto associado a categoria."
                    : 
                    $"Há {category.Products.Count} produtos associado a categoria."
                    );
            }

            if (errors.Any())
                return Result<CategoryOutput>.Fail(errors, "Erro ao deletar categoria.", GetStatusCode(errors));

            await _repository.DeleteAsync(category!);
            await _unitOfWork.CommitAsync();

            var output = new CategoryOutput
            {
                Id = category!.Id,
                Name = category.Name,
                ProductQuantity = category.Products.Count,
                Created = category.Created
            };

            return Result<CategoryOutput>.Ok(output, "Categoria deletada com sucesso.");
        }

        public async Task<Result<ICollection<CategoryOutput>>> GetAsync()
        {
            var categories = await _repository.GetAsync();

            if (categories is null || !categories.Any())
            {
                return Result<ICollection<CategoryOutput>>.Fail(
                    new List<string> { "Nenhuma categoria encontrada." },
                    "Erro ao buscar categorias.",
                    404
                );
            }

            var outputs = categories.Select(c => new CategoryOutput
            {
                Id = c.Id,
                Name = c.Name,
                ProductQuantity = c.Products.Count,
                Created = c.Created
            }).ToList();

            return Result<ICollection<CategoryOutput>>
                .Ok(outputs, "Categorias recuperadas com sucesso.");
        }

        public async Task<Result<CategoryOutput>> UpdateAsync(Guid id, CategoryInput input)
        {
            var errors = new List<string>();

            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                errors.AddRange(validation.Errors.Select(e => e.ErrorMessage));

            var category = await _repository.GetByIdAsync(id);

            if (category is null)
                errors.Add("Categoria não encontrada.");

            if (await _repository.ExistsAsync(input.Name, id))
                errors.Add("Já existe outra categoria com esse nome.");

            if (errors.Any())
                return Result<CategoryOutput>.Fail(errors, "Erro ao atualizar categoria.", GetStatusCode(errors));

            category!.Name = input.Name.Trim();

            await _repository.UpdateAsync(category);
            await _unitOfWork.CommitAsync();

            var output = new CategoryOutput
            {
                Id = category.Id,
                Name = category.Name,
                ProductQuantity = category.Products.Count,
                Created = category.Created
            };

            return Result<CategoryOutput>.Ok(output, "Categoria atualizada com sucesso.");
        }
    }
}