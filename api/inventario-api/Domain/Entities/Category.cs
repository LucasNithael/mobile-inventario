namespace inventario_api.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }

    }
}
