using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;
namespace foodDelivery.Infrustructure;

public class DbManager : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public DbSet<WorkingHour> WorkingHours { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Menu> Menus { get; set; }
    public DbSet<Food> Foods { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;DataBase=FoodDB;Username=postgres;Password=Alireza1383");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>()
            .Property(u => u.Role)
            .HasConversion<string>();
        
        modelBuilder.Entity<Customer>()
            .HasOne(c => c.User)
            .WithOne()
            .HasForeignKey<Customer>(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Vendor>()
            .HasOne(v => v.User)
            .WithOne()
            .HasForeignKey<Vendor>(v => v.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Vendor>()
            .HasMany(v => v.Restaurants)
            .WithOne(r => r.Vendor)
            .HasForeignKey(r => r.VendorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<WorkingHour>()
            .HasOne(w => w.Restaurant)
            .WithMany(r => r.WorkingHours)
            .HasForeignKey(w => w.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Location>()
            .HasOne(l => l.Restaurant)
            .WithOne(r => r.Location)
            .HasForeignKey<Location>(l => l.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Menu>()
            .HasOne(m => m.Restaurant)
            .WithMany(r => r.Menus)
            .HasForeignKey(m => m.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Menu>()
            .Property(m => m.Category)
            .HasConversion<string>();
        
        modelBuilder.Entity<Food>()
            .HasOne(f => f.Menu)
            .WithMany(m => m.Foods)
            .HasForeignKey(f => f.MenuId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Customer>()
            .HasMany(c => c.Orders)
            .WithOne(o => o.Customer)
            .HasForeignKey(o => o.CustomerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Restaurant>()
            .HasMany(r => r.Orders)
            .WithOne(o => o.Restaurant)
            .HasForeignKey(o => o.RestaurantId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Order>()
            .HasMany(o => o.Items)
            .WithOne(i => i.Order)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<OrderItem>()
            .HasOne(i => i.Food)
            .WithMany(f => f.OrderItems)
            .HasForeignKey(i => i.FoodId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Order>()
            .Property(o => o.Status)
            .HasConversion<string>();
    }
}
