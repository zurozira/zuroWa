namespace zuroWa.Core.Domain.EyeMax;

public class Movie
{
    public int Id { get; set; }
    public int TmdbId { get; set; }
    public DateTime AddedAt { get; set; }
    public string? MyComment { get; set; }

    public Movie()
    {
        AddedAt = DateTime.UtcNow; // Use UtcNow instead of Now because it is timezone-neutral
    }
}