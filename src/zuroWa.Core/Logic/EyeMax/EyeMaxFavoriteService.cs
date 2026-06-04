using Microsoft.EntityFrameworkCore;
using zuroWa.Core.Data;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Core.Logic.EyeMax;

public class EyeMaxFavoriteService(AppDbContext appDbContext)
{
    public async Task<List<Movie>> GetAllAsync()
    {
        return await appDbContext.Movies.ToListAsync();
    }

    public async Task AddAsync(Movie movie)
    {
        appDbContext.Movies.Add(movie);

        // Need to save the change
        await appDbContext.SaveChangesAsync();
    }

    public async Task RemoveAsync(int tmdbId)
    {
        var movie = await appDbContext.Movies.FirstOrDefaultAsync(m => m.TmdbId == tmdbId);
        
        if (movie is null) return;
        
        appDbContext.Movies.Remove(movie);

        await appDbContext.SaveChangesAsync();
    }
    
}