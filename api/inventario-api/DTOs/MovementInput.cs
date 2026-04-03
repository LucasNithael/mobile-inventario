using inventario_api.Domain.Entities;
using inventario_api.Domain.Shared;

namespace inventario_api.DTOs
{
    public class MovementInput
    {
        public decimal Quantity { get; set; }
        public Guid ProductId { get; set; }
        public MovementType Type { get; set; }
    }
}
