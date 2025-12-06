using Microsoft.EntityFrameworkCore;
using TRKApp.Models;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Shop> Shops { get; set; }
    public virtual DbSet<ShopCharacteristic> ShopCharacteristics { get; set; }
    public virtual DbSet<Category> Categories { get; set; }
    public virtual DbSet<Contact> Contacts { get; set; }
    public virtual DbSet<RentalSpace> RentalSpaces { get; set; }
    public virtual DbSet<Cart> Carts { get; set; }
    public virtual DbSet<CartItem> CartItems { get; set; }
}