using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Core.Data;

public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=zurowa.db");
        }
    }
}