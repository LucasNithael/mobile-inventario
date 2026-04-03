using FluentValidation;
using inventario_api.Domain.Entities;
using inventario_api.Domain.Shared;
using inventario_api.DTOs;
using inventario_api.Repositories;

namespace inventario_api.Services
{
    public class MovementService(
        IMovementRepository movementRepository,
        IProductRepository productRepository,
        IUnitOfWork unitOfWork,
        IValidator<MovementInput> validator
    ) : IMovementService
    {
        private readonly IMovementRepository _movementRepository = movementRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;
        private readonly IValidator<MovementInput> _validator = validator;

        private static decimal GetStock(Product product)
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

        private static List<string> ValidateStock(Product product, MovementInput input)
        {
            var errors = new List<string>();

            var stock = GetStock(product);

            if (input.Type == MovementType.OUTBOUND && input.Quantity > stock)
                errors.Add($"Estoque insuficiente. Estoque atual: {stock}.");

            return errors;
        }
        private static int GetStatusCode(List<string> errors)
        {
            if (errors.Any(e => e.Contains("não encontrada") || e.Contains("não encontrado")))
                return 404;

            if (errors.Any(e => e.Contains("Já existe")))
                return 409;

            return 400;
        }

        public async Task<Result<MovementOutput>> AddAsync(MovementInput input)
        {
            var errors = new List<string>();

            var validation = await _validator.ValidateAsync(input);

            if (!validation.IsValid)
                errors.AddRange(validation.Errors.Select(e => e.ErrorMessage));

            var product = await _productRepository.GetByIdAsync(input.ProductId);

            if (product is null)
                errors.Add("Produto não encontrado.");

            if (product is not null)
                errors.AddRange(ValidateStock(product, input));

            if (errors.Any())
                return Result<MovementOutput>.Fail(errors, "Erro ao criar movimentação.", GetStatusCode(errors));

            var movement = new Movement
            {
                ProductId = product!.Id,
                Product = product,
                Quantity = input.Quantity,
                Type = input.Type,
                Created = DateTime.UtcNow
            };

            await _movementRepository.AddAsync(movement);
            await _unitOfWork.CommitAsync();

            var output = new MovementOutput
            {
                Id = movement.Id,
                ProductId = movement.ProductId,
                Product = product.Name,
                Quantity = movement.Quantity,
                Type = movement.Type,
                Created = movement.Created
            };

            return Result<MovementOutput>.Created(output, "Movimentação criada com sucesso.");
        }

        public async Task<Result<ICollection<MovementOutput>>> GetAsync()
        {
            var movements = await _movementRepository.GetAsync();

            if (movements is null || !movements.Any())
            {
                return Result<ICollection<MovementOutput>>.Fail(
                    new List<string> { "Nenhuma movimentação encontrada." },
                    "Erro ao buscar movimentações.",
                    404
                );
            }

            var outputs = movements.Select(m => new MovementOutput
            {
                Id = m.Id,
                ProductId = m.ProductId,
                Product = m.Product.Name,
                Quantity = m.Quantity,
                Type = m.Type,
                Created = m.Created
            }).ToList();

            return Result<ICollection<MovementOutput>>
                .Ok(outputs, "Movimentações recuperadas com sucesso.");
        }

        public async Task<Result<ICollection<MovementOutput>>> GetByProductIdAsync(Guid productId)
        {
            var errors = new List<string>();

            var product = await _productRepository.GetByIdAsync(productId);

            if (product is null)
                errors.Add("Produto não encontrado.");

            if (errors.Any())
            {
                return Result<ICollection<MovementOutput>>.Fail(
                    errors,
                    "Erro ao buscar movimentações do produto.",
                    GetStatusCode(errors)
                );
            }

            var movements = await _movementRepository.GetByProductIdAsync(productId);

            if (movements is null || !movements.Any())
            {
                return Result<ICollection<MovementOutput>>.Fail(
                    new List<string> { "Nenhuma movimentação encontrada para este produto." },
                    "Erro ao buscar movimentações do produto.",
                    404
                );
            }

            var outputs = movements.Select(m => new MovementOutput
            {
                Id = m.Id,
                ProductId = m.ProductId,
                Product = m.Product.Name,
                Quantity = m.Quantity,
                Type = m.Type,
                Created = m.Created
            }).ToList();

            return Result<ICollection<MovementOutput>>
                .Ok(outputs, "Movimentações do produto recuperadas com sucesso.");
        }
    }
}