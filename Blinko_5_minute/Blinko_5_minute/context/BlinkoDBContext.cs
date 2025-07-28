using Blinko_5_minute.darkStore;
using Blinko_5_minute.model;
using Microsoft.EntityFrameworkCore;

namespace Blinko_5_minute.context
{
    public class BlinkoDBContext:DbContext
    {
        public BlinkoDBContext(DbContextOptions<BlinkoDBContext> options)
         : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder.Entity<DarkStore>()
                .HasKey(ds => ds.id);

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Sku);

            modelBuilder.Entity<Product>()
                .Property(p => p.Sku)
                .IsRequired();

            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);

            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Cart)
                .WithOne(cart => cart.Customer)
                .HasForeignKey<Cart>(c => c.CustomerId);

            // Seed data for Customers  
            // Seed data for Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    CustomerId = 1,
                    Name = "John Doe",
                    Email = "john.doe@example.com",
                    Address = "123 Main St, Springfield",
                    PhoneNumber = "123-456-7890",
                    X_Cord = 40.7128,
                    Y_Cord = -74.0060
                },
                new Customer
                {
                    CustomerId = 2,
                    Name = "Jane Smith",
                    Email = "jane.smith@example.com",
                    Address = "456 Elm St, Springfield",
                    PhoneNumber = "987-654-3210",
                    X_Cord = 34.0522,
                    Y_Cord = -118.2437
                });

            // Seed data for Products  
            modelBuilder.Entity<Product>().HasData(
            new Product { Sku = 1, Name = "Laptop", Price = 999.99, Category = "Electronics", quantity = 50 },
            new Product { Sku = 2, Name = "Smartphone", Price = 699.99, Category = "Electronics", quantity = 100 },
            new Product { Sku = 3, Name = "Desk Chair", Price = 89.99, Category = "Furniture", quantity = 30 },
            new Product { Sku = 4, Name = "Notebook", Price = 2.99, Category = "Stationery", quantity = 200 },
            new Product { Sku = 5, Name = "Headphones", Price = 49.99, Category = "Electronics", quantity = 75 }
            );

            // Seed data for Carts  
            modelBuilder.Entity<Cart>().HasData(
                new Cart { CartId = 1, CustomerId = 1 },
                new Cart { CartId = 2, CustomerId = 2 }
            );

            // Seed data for CartItems  
            modelBuilder.Entity<CartItem>().HasData(
                new CartItem { CartItemId = 1, CartId = 1, ProductId = 1, Quantity = 2 },
                new CartItem { CartItemId = 2, CartId = 1, ProductId = 2, Quantity = 1 },
                new CartItem { CartItemId = 3, CartId = 2, ProductId = 1, Quantity = 3 }
            );

            // Seed data for DarkStores  
            // Seed data for DarkStore
            modelBuilder.Entity<DarkStore>().HasData(
                new DarkStore { id = 1, _Xcord = 40.7128, _Ycord = -74.0060, _name = "Central DarkStore" },
                new DarkStore { id = 2, _Xcord = 34.0522, _Ycord = -118.2437, _name = "West DarkStore" },
                new DarkStore { id = 3, _Xcord = 51.5074, _Ycord = -0.1278, _name = "London DarkStore" }
            );
        }
        public DbSet<Product> Products { get; set; }
         public DbSet<Order> Orders { get; set; }
         public DbSet<DarkStore> DarkStores { get; set; }

        public DbSet<DeliveryPartener> DeliveryParteners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<User> Users { get; set; }
        // Add other DbSets as needed
    }
}
