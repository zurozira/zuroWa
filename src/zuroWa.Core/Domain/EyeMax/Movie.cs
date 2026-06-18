namespace zuroWa.Core.Domain.EyeMax;

public class Movie
{
    public int Id { get; set; }
    public int TmdbId { get; set; }
    public DateTime AddedAt { get; set; } = DateTime.UtcNow; // Use UtcNow instead of Now because it is timezone-neutral
    public string? MyComment { get; set; } // MyComment being nullable (string?) since it's optional
    public string Title { get; set; } = string.Empty;
    public string PosterPath { get; set; } = string.Empty;

    public string UserId { get; set; } = string.Empty;

    public string SavedBy { get; set; } = string.Empty;
}