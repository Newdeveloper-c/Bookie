using Bookie.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bookie.DataAccess.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Classic", DisplayOrder = 1 },
            new Category { Id = 2, Name = "Fiction", DisplayOrder = 2 },
            new Category { Id = 3, Name = "Biography", DisplayOrder = 3 });
        
        base.OnModelCreating(modelBuilder);
    }
}
