using Microsoft.EntityFrameworkCore;
using TransportControl.Model.Entity;

public class ApplicationDbContext : DbContext
{
    public DbSet<TrackList> TrackLists { get; set; }
    public DbSet<TrackPoint> TrackPoints { get; set; }
    public DbSet<Car> Cars { get; set; }
    public DbSet<Driver> Drivers { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TrackList>()
           .HasMany(t => t.TrackPoints)
           .WithOne(tp => tp.TrackList)
           .HasForeignKey(tp => tp.TrackListId);

        modelBuilder.Entity<TrackList>()
            .HasOne(t => t.Car)
            .WithMany()
            .HasForeignKey(t => t.CarId);

        modelBuilder.Entity<TrackList>()
            .HasOne(t => t.Driver)
            .WithMany()
            .HasForeignKey(t => t.DriverId);
    }
}