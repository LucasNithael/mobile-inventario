namespace inventario_api.Domain.Entities
{
    public class Product : Entity
    {
        public required string Name { get; set; }
        public decimal MinimumQuantity { get; set; }
        public Guid CategoryId { get; set; }
        public required Category Category { get; set; }
        public ICollection<Movement>? Movements { get; set; }

    }
}
