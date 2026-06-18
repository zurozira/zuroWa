using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Data;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Core.Logic.EyeMax;

public class EyeMaxFavoriteService(AppDbContext appDbContext)
{
    public async Task<List<Movie>> GetAllAsync()
    {
        // Order helps show newest added first
        return await appDbContext.Movies
            .OrderByDescending(m => m.AddedAt)
            .ToListAsync();
    }

    public async Task AddAsync(Movie movie)
    {
        appDbContext.Movies.Add(movie);

        // Need to save the change
        await appDbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int id, string userId)
    {
        var movie = await appDbContext.Movies.FirstOrDefaultAsync(m => m.Id == id);
        
        if (movie is null) return;

        if (movie.UserId == userId)
        {
            appDbContext.Movies.Remove(movie);

            await appDbContext.SaveChangesAsync();
        }
    }
}