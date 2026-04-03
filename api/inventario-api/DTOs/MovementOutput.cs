using inventario_api.Domain.Shared;

namespace inventario_api.DTOs
{
    public class MovementOutput
    {
        public Guid Id { get; set; }
        public decimal Quantity { get; set; }
        public Guid ProductId { get; set; }
        public string Product { get; set; }
        public MovementType Type { get; set; }
        public DateTime Created { get; set; }
    }
}
