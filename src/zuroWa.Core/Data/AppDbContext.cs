using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Core.Data;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext
{
    // A DbSet<T> property is how EF Core knows to create a table for that entity.
    // The property name becomes the table name by convention so DbSet<Movie> Movies → table called Movies.
    public DbSet<Movie> Movies { get; set; } = null!;
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=zurowa.db");
        }
    }
}