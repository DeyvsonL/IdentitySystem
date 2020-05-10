using IdentitySystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace IdentitySystem.API.DAL
{
    public class IdentitySystemDbContext : DbContext
    {
        public IdentitySystemDbContext(DbContextOptions options) : base(options) { }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();
        }
    }
}
