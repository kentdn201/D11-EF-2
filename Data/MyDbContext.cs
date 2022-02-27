using D11.Data.Entites;
using Microsoft.EntityFrameworkCore;

namespace D11.Data
{
    public class MyDbContext : DbContext
    {
        public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Category
            builder.Entity<Category>(e => e.ToTable("Categories"));

            // builder.Entity<Category>().HasKey(e => e.Id);

            builder.Entity<Category>()
            .HasMany(category => category.Products)
            .WithOne(product => product.Category)
            .HasForeignKey(product => product.CategoryId)
            .IsRequired();

            var data = new List<Category>
            {
                new Category{Id=1, Name="Food"},
                new Category{Id=2, Name="Drinks"},
                new Category{Id=3, Name="Cosmetics"},
                new Category{Id=4, Name="Films"}
            };
            builder.Entity<Category>().HasData(data);

            // Product
            builder.Entity<Product>(e => e.ToTable("Products"));
        }

        public virtual DbSet<Student>? Students { get; set; }

        // public virtual DbSet<Category>? Categories { get; set; }
        // public virtual DbSet<Product>? Products { get; set; }
    }
}