using inventario_api.Domain.Shared;

namespace inventario_api.Domain.Entities
{
    public class Movement : Entity
    {
        public decimal Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public MovementType Type { get; set; }

    }
}
