using Blinko_5_minute.darkStore;
using Blinko_5_minute.model;
using System.Data.Entity;

namespace Blinko_5_minute.context
{
    public class BlinkoDBContext:DbContext
    {

        public BlinkoDBContext() : base("name=BlinkoDBContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Configure your entity mappings here
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>()
                .HasKey(p => p.Sku)
                .Property(p => p.Sku)
                .IsRequired();
            modelBuilder.Entity<Cart>()
                .HasKey(c => c.CartId);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.items)
                .WithOne(ci => ci.Cart)
                .HasForeignKey<CartItem>(c => c.CartId);

            modelBuilder.Entity<Customer>()
                .HasKey(c => c.CustomerId);
            modelBuilder.Entity<Customer>()
                .HasOne(c => c.Cart)
                .WithOne( c => c.Customer)
                .HasForeignKey<Cart>(c => c.CustomerId);

        }

         public DbSet<Product> Products { get; set; }
         public DbSet<Order> Orders { get; set; }
         public DbSet<DarkStore> DarkStores { get; set; }

        public DbSet<DeliveryPartener> DeliveryParteners { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        // Add other DbSets as needed
    }
}
