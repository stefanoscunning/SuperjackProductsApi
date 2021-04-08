using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace SuperjackProducts.Api.DataAccess
{
  public class AppDbContext : DbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductCategory> ProductCategories { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<Tag> Tags { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      modelBuilder.Entity<ProductCategory>()
                    .HasKey(s => new { s.ProductId, s.CategoryId });

      modelBuilder.Entity<ProductCategory>()
        .HasOne(pc => pc.Product)
        .WithMany(p => p.ProductCategories)
        .HasForeignKey(pc => pc.ProductId);

      modelBuilder.Entity<ProductCategory>()
          .HasOne(pc => pc.Category)
          .WithMany(c => c.ProductCategories)
          .HasForeignKey(pc => pc.CategoryId);

      modelBuilder.Entity<ProductTag>()
                    .HasKey(s => new { s.ProductId, s.TagId });

      modelBuilder.Entity<ProductTag>()
       .HasOne(pc => pc.Product)
       .WithMany(p => p.ProductTags)
       .HasForeignKey(pc => pc.ProductId);

      modelBuilder.Entity<ProductTag>()
          .HasOne(pc => pc.Tag)
          .WithMany(c => c.ProductTags)
          .HasForeignKey(pc => pc.TagId);

      var categories = new Category[]
      {
        new Category(){Id=1, Title="Clothing"},
        new Category(){Id=2, Title="Tops"},
        new Category(){Id=3, Title="Trousers"}
      };    

      modelBuilder.Entity<Category>().HasData(categories);

      var languages = new Language[]
      {
        new Language(){Id=1, Title="English (United Kingdom)", Culture="en-GB"},
        new Language(){Id=2, Title="Greek", Culture="el"}
      };

      modelBuilder.Entity<Language>().HasData(languages);

      var tags = new Tag[]
      {
        new Tag(){Id=1, Title="Long-sleeved"},
        new Tag(){Id=2, Title="Mens"},
        new Tag(){Id=3, Title="Polo"},
        new Tag(){Id=4, Title="Short-sleeved"},
        new Tag(){Id=5, Title="T-Shirts"},
        new Tag(){Id=6, Title="Womens"}
      };

      modelBuilder.Entity<Tag>().HasData(tags);

      var manufacturers = new Manufacturer[]
      {
        new Manufacturer(){Id=1, Title="Gucci"},
        new Manufacturer(){Id=2, Title="Ralph Lauren"},
        new Manufacturer(){Id=3, Title="Tom Ford"}
      };

      modelBuilder.Entity<Manufacturer>().HasData(manufacturers);

      var products = new Product[]
     {
        new Product(){Id=1, ManufacturerId=2, Name="Custom Fit" },
        new Product(){Id=2, ManufacturerId=2, Name="Slim Fit"},
        new Product(){Id=3, ManufacturerId=3, Name="Regular-fit Silk and Cotton Blend"}
     };

      modelBuilder.Entity<Product>().HasData(products);

      var productCategories = new ProductCategory[]
      {
        new ProductCategory(){ProductId=1, CategoryId=1},
        new ProductCategory(){ProductId=1, CategoryId=2},
        new ProductCategory(){ProductId=2, CategoryId=1},
        new ProductCategory(){ProductId=2, CategoryId=2},
        new ProductCategory(){ProductId=3, CategoryId=1},
        new ProductCategory(){ProductId=3, CategoryId=2}
      };

      modelBuilder.Entity<ProductCategory>().HasData(productCategories);

      var productTags = new ProductTag[]
      {
        new ProductTag(){ProductId=1, TagId=2},
        new ProductTag(){ProductId=1, TagId=3},
        new ProductTag(){ProductId=1, TagId=4},
        new ProductTag(){ProductId=2, TagId=2},
        new ProductTag(){ProductId=2, TagId=3},
        new ProductTag(){ProductId=2, TagId=4},
        new ProductTag(){ProductId=3, TagId=1},
        new ProductTag(){ProductId=3, TagId=2}
      };

      modelBuilder.Entity<ProductTag>().HasData(productTags);

      base.OnModelCreating(modelBuilder);

    }

    private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {

      if (password == null) throw new ArgumentNullException("password");
      if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

      using (var hmac = new HMACSHA512())
      {
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
      }
    }



  }
}
