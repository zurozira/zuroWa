using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Core.Data;

public class EyeMaxFavoriteRepository(AppDbContext appDbContext)
{
    public async Task<List<Movie>> GetAllFavorite()
    {
        List<Movie> movies = await appDbContext.
    }
}