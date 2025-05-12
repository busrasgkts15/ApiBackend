using Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiBackend.Context;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    public DbSet<Category> categories { get; set; }
    public DbSet<Product> products { get; set; }
    public DbSet<Role> roles { get; set; }
    public DbSet<Support> supports { get; set; }
    public DbSet<User> users { get; set; }
    public DbSet<UserRole> userRoles { get; set; }
}