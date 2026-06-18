using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Domain;
using zuroWa.Core.Domain.EyeMax;
using zuroWa.Core.Domain.Ponsai;
using zuroWa.Core.Domain.ZicZacZu;

namespace zuroWa.Core.Data;

// AppDbContext is the single gateway between C# code and DB
public class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : IdentityDbContext<AppUser>(dbContextOptions)
{
    
    // EYEMAX
    // A DbSet<T> property is how EF Core knows to create a table for that entity.
    // The property name becomes the table name by convention so DbSet<Movie> Movies → table called Movies.
    public DbSet<Movie> Movies { get; set; } = null!; // Each DBSet represents one table
    
    // Configure to use SQLite Db
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) // Means: only use this fallback connection string if nothing was injected
        {
            optionsBuilder.UseSqlite("Data Source=zurowa.db");
        }
    }
    
    // PONSAI
    // I added Habit and Habit log db sets
    public DbSet<Habit> Habits { get; set; } = null!;
    public DbSet<HabitLog> HabitLogs { get; set; } = null!;
    
    // EF Core handle most things automatically
    // Property name Id -> primary key
    // Property named HabitId on HabitLog -> foreign key to Habits table
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // IdentityDbContext has its own OnModelCreating
        // that sets up all Identity tables: AspNetUsers, AspNetRoles,...
        // Those tables never get configured if we dont call base.OnModelCreating(builder)
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<HabitLog>() // Configuring HabitLog table
            .HasIndex(h => new { h.HabitId, h.LoggedOn }) // Create a composite index on these 2 columns
            .IsUnique(); // Db should reject any row where this combination already exists
    }
    
    // ZICZACZU
    public DbSet<Game> Games { get; set; } = null!;
}