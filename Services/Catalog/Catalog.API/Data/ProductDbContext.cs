using Catalog.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Data;

public class ProductDbContext : DbContext
{
    public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var mensClothingId = Guid.NewGuid();
        var womensClothingId = Guid.NewGuid();
        var kidsClothingId = Guid.NewGuid();
        var footwearId = Guid.NewGuid();
        var accessoriesId = Guid.NewGuid();
        var sportswearId = Guid.NewGuid();
        var loungewearId = Guid.NewGuid();
        var outerwearId = Guid.NewGuid();
        var formalWearId = Guid.NewGuid();
        var underwearId = Guid.NewGuid();


        var product1Id = Guid.NewGuid();
        var product2Id = Guid.NewGuid();
        var product3Id = Guid.NewGuid();
        var product4Id = Guid.NewGuid();
        var product5Id = Guid.NewGuid();
        var product6Id = Guid.NewGuid();
        var product7Id = Guid.NewGuid();
        var product8Id = Guid.NewGuid();
        var product9Id = Guid.NewGuid();
        var product10Id = Guid.NewGuid();
        var product11Id = Guid.NewGuid();
        var product12Id = Guid.NewGuid();
        var product13Id = Guid.NewGuid();
        var product14Id = Guid.NewGuid();
        var product15Id = Guid.NewGuid();
        var product16Id = Guid.NewGuid();
        var product17Id = Guid.NewGuid();
        var product18Id = Guid.NewGuid();
        var product19Id = Guid.NewGuid();
        var product20Id = Guid.NewGuid();

        modelBuilder.Entity<Category>(c =>
        {
            c.HasKey(c => c.Id);
            c.Property(c => c.Name).HasMaxLength(300);
            c.Property(c => c.Description).HasMaxLength(1000);

            c.HasData(
            new Category { Id = mensClothingId, Name = "Men's Clothing", Description = "Apparel for men" },
            new Category { Id = womensClothingId, Name = "Women's Clothing", Description = "Apparel for women" },
            new Category { Id = kidsClothingId, Name = "Kids' Clothing", Description = "Apparel for children" },
            new Category { Id = footwearId, Name = "Footwear", Description = "Shoes, boots, sneakers" },
            new Category { Id = accessoriesId, Name = "Accessories", Description = "Belts, hats, scarves, bags" },
            new Category { Id = sportswearId, Name = "Sportswear", Description = "Activewear, gym clothes" },
            new Category { Id = loungewearId, Name = "Loungewear", Description = "Comfortable home clothing" },
            new Category { Id = outerwearId, Name = "Outerwear", Description = "Jackets, coats" },
            new Category { Id = formalWearId, Name = "Formal Wear", Description = "Suits, dresses, gowns" },
            new Category { Id = underwearId, Name = "Underwear & Sleepwear", Description = "Underwear, pajamas, nightwear" });
        });

        modelBuilder.Entity<Product>(p =>
        {
            p.HasKey(p => p.Id);
            p.Property(p => p.Name).HasMaxLength(300);
            p.Property(p => p.Description).HasMaxLength(1000);

            p.HasData(
                new Product { Id = product1Id, Name = "Men's Classic T-Shirt", Description = "100% cotton, comfortable fit", Sku = "MEN-TSHIRT-01", Price = 25.99m, Rating = 4, IsAvailable = true, ImageFile = "mens-classic-tshirt.jpg" },
    new Product { Id = product2Id, Name = "Men's Slim Jeans", Description = "Denim, slim fit, dark wash", Sku = "MEN-JEANS-02", Price = 49.99m, Rating = 5, IsAvailable = true, ImageFile = "mens-slim-jeans.jpg" },
    new Product { Id = product3Id, Name = "Women's Summer Dress", Description = "Lightweight, floral print", Sku = "WOM-DRESS-01", Price = 39.99m, Rating = 5, IsAvailable = true, ImageFile = "womens-summer-dress.jpg" },
    new Product { Id = product4Id, Name = "Women's Blouse", Description = "Silk blend, long sleeves", Sku = "WOM-BLOUSE-02", Price = 35.50m, Rating = 4, IsAvailable = true, ImageFile = "womens-blouse.jpg" },
    new Product { Id = product5Id, Name = "Kids' Hoodie", Description = "Soft cotton, colorful design", Sku = "KID-HOODIE-01", Price = 29.99m, Rating = 4, IsAvailable = true, ImageFile = "kids-hoodie.jpg" },
    new Product { Id = product6Id, Name = "Kids' Sneakers", Description = "Lightweight and durable", Sku = "KID-SNEAK-01", Price = 34.99m, Rating = 5, IsAvailable = true, ImageFile = "kids-sneakers.jpg" },
    new Product { Id = product7Id, Name = "Leather Belt", Description = "Genuine leather, classic buckle", Sku = "ACC-BELT-01", Price = 19.99m, Rating = 4, IsAvailable = true, ImageFile = "leather-belt.jpg" },
    new Product { Id = product8Id, Name = "Men's Running Shoes", Description = "Lightweight with cushioned sole", Sku = "MEN-RUN-01", Price = 89.99m, Rating = 5, IsAvailable = true, ImageFile = "mens-running-shoes.jpg" },
    new Product { Id = product9Id, Name = "Women's Yoga Pants", Description = "Stretchable and comfortable", Sku = "WOM-YOGA-01", Price = 45.00m, Rating = 4, IsAvailable = true, ImageFile = "womens-yoga-pants.jpg" },
    new Product { Id = product10Id, Name = "Winter Jacket", Description = "Waterproof and insulated", Sku = "OUT-JACKET-01", Price = 129.99m, Rating = 5, IsAvailable = true, ImageFile = "winter-jacket.jpg" },
    new Product { Id = product11Id, Name = "Men's Suit", Description = "Tailored fit, premium fabric", Sku = "FORM-SUIT-01", Price = 249.99m, Rating = 5, IsAvailable = true, ImageFile = "mens-suit.jpg" },
    new Product { Id = product12Id, Name = "Women's Evening Gown", Description = "Elegant floor-length gown", Sku = "FORM-GOWN-01", Price = 199.99m, Rating = 5, IsAvailable = true, ImageFile = "womens-evening-gown.jpg" },
    new Product { Id = product13Id, Name = "Loungewear Set", Description = "Soft cotton top and pants", Sku = "LOUNGE-SET-01", Price = 59.99m, Rating = 4, IsAvailable = true, ImageFile = "loungewear-set.jpg" },
    new Product { Id = product14Id, Name = "Men's Pajama Set", Description = "Comfortable nightwear", Sku = "UNDER-MEN-01", Price = 39.99m, Rating = 4, IsAvailable = true, ImageFile = "mens-pajama-set.jpg" },
    new Product { Id = product15Id, Name = "Women's Pajama Set", Description = "Soft and cozy sleepwear", Sku = "UNDER-WOM-01", Price = 42.99m, Rating = 5, IsAvailable = true, ImageFile = "womens-pajama-set.jpg" },
    new Product { Id = product16Id, Name = "Kids' Winter Boots", Description = "Waterproof and insulated", Sku = "KID-BOOT-01", Price = 59.99m, Rating = 5, IsAvailable = true, ImageFile = "kids-winter-boots.jpg" },
    new Product { Id = product17Id, Name = "Sports Cap", Description = "Breathable material, adjustable", Sku = "ACC-CAP-01", Price = 14.99m, Rating = 4, IsAvailable = true, ImageFile = "sports-cap.jpg" },
    new Product { Id = product18Id, Name = "Sneakers for Women", Description = "Casual and sporty", Sku = "WOM-SNEAK-01", Price = 79.99m, Rating = 4, IsAvailable = true, ImageFile = "womens-sneakers.jpg" },
    new Product { Id = product19Id, Name = "Backpack", Description = "Durable and spacious", Sku = "ACC-BAG-01", Price = 49.99m, Rating = 4, IsAvailable = true, ImageFile = "backpack.jpg" },
    new Product { Id = product20Id, Name = "Men's Hoodie", Description = "Warm and stylish", Sku = "MEN-HOODIE-01", Price = 59.99m, Rating = 4, IsAvailable = true, ImageFile = "mens-hoodie.jpg" });
        });

        modelBuilder.Entity<ProductCategory>().HasData(
             new ProductCategory { ProductId = product1Id, CategoryId = mensClothingId },
            new ProductCategory { ProductId = product2Id, CategoryId = mensClothingId },
            new ProductCategory { ProductId = product3Id, CategoryId = womensClothingId },
            new ProductCategory { ProductId = product4Id, CategoryId = womensClothingId },
            new ProductCategory { ProductId = product5Id, CategoryId = kidsClothingId },
            new ProductCategory { ProductId = product6Id, CategoryId = kidsClothingId },
            new ProductCategory { ProductId = product7Id, CategoryId = accessoriesId },
            new ProductCategory { ProductId = product8Id, CategoryId = mensClothingId },
            new ProductCategory { ProductId = product9Id, CategoryId = womensClothingId },
            new ProductCategory { ProductId = product10Id, CategoryId = outerwearId },
            new ProductCategory { ProductId = product11Id, CategoryId = mensClothingId },
            new ProductCategory { ProductId = product12Id, CategoryId = womensClothingId },
            new ProductCategory { ProductId = product13Id, CategoryId = loungewearId },
            new ProductCategory { ProductId = product14Id, CategoryId = underwearId },
            new ProductCategory { ProductId = product15Id, CategoryId = underwearId },
            new ProductCategory { ProductId = product16Id, CategoryId = kidsClothingId },
            new ProductCategory { ProductId = product17Id, CategoryId = accessoriesId },
            new ProductCategory { ProductId = product18Id, CategoryId = footwearId },
            new ProductCategory { ProductId = product19Id, CategoryId = accessoriesId },
            new ProductCategory { ProductId = product20Id, CategoryId = mensClothingId });

        modelBuilder.Entity<ProductCategory>()
            .HasKey(pc => new { pc.ProductId, pc.CategoryId });

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCategories)
            .HasForeignKey(pc => pc.ProductId);

        modelBuilder.Entity<ProductCategory>()
            .HasOne(pc => pc.Category)
            .WithMany(c => c.ProductCategories)
            .HasForeignKey(pc => pc.CategoryId);
    }
}
