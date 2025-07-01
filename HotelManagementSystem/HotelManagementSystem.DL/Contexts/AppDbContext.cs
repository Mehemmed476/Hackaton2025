using HotelManagementSystem.Core.Entities;
using HotelManagementSystem.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HotelManagementSystem.DL.Contexts;

public class AppDbContext : IdentityDbContext<AppUser, IdentityRole, string>
{
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Room> Rooms { get; set; }
    public DbSet<Reservation> Reservations { get; set; }
    public DbSet<Service> Services { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "6d16558a-ba79-4fb5-9717-bd333cfc2b0d", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "287f30c4-6d0f-4687-b117-49d810376603", Name = "Manager", NormalizedName = "MANAGER" }
        );



        AppUser admin = new()
        {
            Id = "c10c9801-9957-4018-8e48-0c7812d47b50",
            UserName = "admin",
            NormalizedUserName = "ADMIN",
        };

        PasswordHasher<AppUser> hasher = new();
        admin.PasswordHash = hasher.HashPassword(admin, "admin123!@");

        builder.Entity<AppUser>().HasData(admin);

        builder.Entity<IdentityUserRole<string>>().HasData(
            new IdentityUserRole<string> { UserId = admin.Id, RoleId = "6d16558a-ba79-4fb5-9717-bd333cfc2b0d" }
        );

        base.OnModelCreating(builder);
    }
}
