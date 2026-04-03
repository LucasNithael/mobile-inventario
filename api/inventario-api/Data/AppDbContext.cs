using inventario_api.Domain.Entities;
using inventario_api.Domain.Shared;
using Microsoft.EntityFrameworkCore;

namespace inventario_api.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Movement> Movements { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // CATEGORY
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(c => c.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(c => c.Updated)
                    .IsRequired(false);

                entity.Property(c => c.Created)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRequired();
            });

            // PRODUCT
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(p => p.Name)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(p => p.Updated)
                    .IsRequired(false);

                entity.Property(p => p.Created)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRequired();
            });

            // MOVEMENT
            modelBuilder.Entity<Movement>(entity =>
            {
                
                entity.Property(m => m.Quantity)
                    .HasDefaultValue(0)
                    .IsRequired();

                entity.Property(m => m.Type)
                    .HasDefaultValue(MovementType.INBOUND)
                    .IsRequired();

                entity.Property(m => m.Updated)
                    .IsRequired(false);

                entity.Property(m => m.Created)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP")
                    .IsRequired();
            });

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deleting a category if it has products

            modelBuilder.Entity<Movement>()
                .HasOne(m => m.Product)
                .WithMany(p => p.Movements)
                .HasForeignKey(m => m.ProductId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
