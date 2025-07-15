using foodDelivery.Domain;
using Microsoft.EntityFrameworkCore;
namespace foodDelivery.Infrustructure;

public class DbManager : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Host=localhost;DataBase=FoodDB;Username=postgres;Password=Alireza1383");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<User>().HasDiscriminator<string>("Role")
            .HasValue<Customer>("Customer")
            .HasValue<Vendor>("Vendor")
            .HasValue<Admin>("Admin");
    }
    
}