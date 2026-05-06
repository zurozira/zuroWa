using Newtonsoft.Json.Linq;
using zuroWa.Core.Domain.EyeMax;

namespace zuroWa.Core.Logic;

public class EyeMaxTmdbService
{
    private const string apiKey = "50f4799410a1c57dc37e5d0e37b02f7d";
    private const string baseURL = "https://api.themoviedb.org/3";

    private static readonly HttpClient client = new HttpClient();

    private string BuildUrl(string title)
    {
        return $"{baseURL}/search/movie?api_key={apiKey}&query={title}&include_adult=false&language=en-US&page=1";
    }

    public async Task<List<TmdbMovie>> SearchMoviesAsync(string title)
    {
        List<TmdbMovie> movies = new List<TmdbMovie>();

        var request = new HttpRequestMessage(HttpMethod.Get, BuildUrl(title));
        request.Headers.Add("Accept", "application/json");

        try
        {
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadAsStringAsync();
            var results = JObject.Parse(payload)["results"];

            if (results == null)
                return movies;

            TmdbMovie movie;
            foreach (var result in results)
            {
                movie = new TmdbMovie();

                if (result["id"] == null)
                    continue;

                if (!int.TryParse(result["id"].ToString(), out int id))
                    continue;

                movie.Id = id;
                movie.Title = result["title"]?.ToString() ?? "";
                movie.ReleaseDate = result["release_date"]?.ToString() ?? "";
                movie.Overview = result["overview"]?.ToString() ?? "";
                movie.PosterPath = result["poster_path"]?.ToString();

                movies.Add(movie);
            }
            return movies;
        }
        catch (Exception)
        {
            return movies;
        }
    }

    public async Task<int?> GetMovieIdAsync(string title)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, BuildUrl(title));
        request.Headers.Add("Accept", "application/json");

        try
        {
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadAsStringAsync();
            var results = JObject.Parse(payload)["results"];

            if (!results.HasValues)
            {
                return null;
            }

            return int.Parse(results[0]["id"].ToString());
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task<string?> GetPosterUrlAsync(string title, string size)
    {
        const string imageBaseUrl = "https://image.tmdb.org/t/p/w92";
        const string largeImageBaseUrl = "https://image.tmdb.org/t/p/w342";

        var request = new HttpRequestMessage(HttpMethod.Get, BuildUrl(title));
        request.Headers.Add("Accept", "application/json");

        try
        {
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var payload = await response.Content.ReadAsStringAsync();
            var results = JObject.Parse(payload)["results"];

            if (!results.HasValues)
            {
                return null;
            }

            var posterPath = results[0]["poster_path"]?.ToString();

            if (posterPath == null)
                return null;

            switch (size)
            {
                case "small":
                    return $"{imageBaseUrl}{posterPath}";
                case "large":
                    return $"{largeImageBaseUrl}{posterPath}";
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }

        return null;
    }
}
