using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using zuroWa.Core.Data;

namespace zuroWa.Web;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<AppDbContext> builder = new DbContextOptionsBuilder<AppDbContext>();

        builder.UseSqlite("Data Source=zurowa.db");

        AppDbContext dbContext = new AppDbContext(builder.Options);

        return dbContext;
    }
}