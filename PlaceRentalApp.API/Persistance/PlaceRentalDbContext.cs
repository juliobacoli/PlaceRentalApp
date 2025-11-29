using Microsoft.EntityFrameworkCore;
using PlaceRentalApp.API.Entities;

namespace PlaceRentalApp.API.Persistance;

public class PlaceRentalDbContext : DbContext
{
    public PlaceRentalDbContext(DbContextOptions<PlaceRentalDbContext> options) : base(options)
    {
    }

    public DbSet<Place> Places { get; set; }
    public DbSet<PlaceAmenity> PlaceAmenities { get; set; }
    public DbSet<PlaceBook> PlaceBooks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<PlaceComment> Comments { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Place>(e =>
        {
            e.HasKey(p => p.Id);

            e.HasMany(p => p.Amenities)
             .WithOne()
             .HasForeignKey(a => a.IdPlace)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasMany(p => p.Books)
             .WithOne(b => b.Place)
             .HasForeignKey(a => a.IdPlace)
             .OnDelete(DeleteBehavior.Restrict);

            e.HasOne(p => p.User)
             .WithMany(u => u.Places)
             .HasForeignKey(p => p.CreatedBy)
             .OnDelete(DeleteBehavior.Restrict);

            e.OwnsOne(p => p.Address, a =>
            {
                a.Property(ad => ad.Street).HasColumnName("Street");
                a.Property(ad => ad.Number).HasColumnName("Number");
                a.Property(ad => ad.ZipCode).HasColumnName("ZipCode");
                a.Property(ad => ad.District).HasColumnName("District");
                a.Property(ad => ad.City).HasColumnName("City");
                a.Property(ad => ad.State).HasColumnName("State");
                a.Property(ad => ad.Country).HasColumnName("Country");
            });
        });

        builder.Entity<PlaceAmenity>(e =>
        {
            e.HasKey(pa => pa.Id);
        });

        builder.Entity<PlaceBook>(e =>
        {
            e.HasKey(pb => pb.Id);
        });

        builder.Entity<User>(e =>
        {
            e.HasKey(u => u.Id);

            e.HasMany(u => u.Books)
             .WithOne(pb => pb.User)
             .HasForeignKey(pb => pb.IdUser)
             .OnDelete(DeleteBehavior.Restrict);
        });

        builder.Entity<PlaceComment>(e =>
        {
            e.HasKey(pc => pc.Id);
            e.HasOne(pc => pc.User)
             .WithMany()
             .HasForeignKey(pc => pc.IdUser)
             .OnDelete(DeleteBehavior.Restrict);
            e.HasOne(pc => pc.Place)
             .WithMany(p => p.Comments)
             .HasForeignKey(pc => pc.IdPlace)
             .OnDelete(DeleteBehavior.Restrict);
        });


        base.OnModelCreating(builder);
    }
}
