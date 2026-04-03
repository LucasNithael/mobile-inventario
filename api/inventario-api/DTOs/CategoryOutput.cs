namespace inventario_api.DTOs
{
    public class CategoryOutput
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal ProductQuantity { get; set; }
        public DateTime Created { get; set; }
    }
}
