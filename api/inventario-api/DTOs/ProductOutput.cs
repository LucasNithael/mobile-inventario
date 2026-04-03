namespace inventario_api.DTOs
{
    public class ProductOutput
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Category { get; set; }
        public decimal Quantity { get; set; }
        public decimal MinimumQuantity { get; set; }
        public DateTime Created { get; set; }
        
    }
}
