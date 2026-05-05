namespace zuroWa.Core.Domain;

public class TmdbMovie
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Overview { get; set; }
    public string ReleaseDate { get; set; }
    public string PosterPath { get; set; }

    public TmdbMovie() {}

    public TmdbMovie(int id, string title, string overview, string releaseDate, string posterPath)
    {
        Id = id;
        Title = title;
        Overview = overview;
        ReleaseDate = releaseDate;
        PosterPath = posterPath;
    }
}