using inventario_api.Domain.Shared;

namespace inventario_api.DTOs
{
    public class ProductInput
    {
        public required string Name { get; set; }
        public decimal MinimumQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
