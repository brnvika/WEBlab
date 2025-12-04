using Microsoft.EntityFrameworkCore;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Review> Reviews { get; set; }
    public virtual DbSet<Shop> Shops { get; set; }
    public virtual DbSet<ShopCharacteristic> ShopCharacteristics { get; set; }
}