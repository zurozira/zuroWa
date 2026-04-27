using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using Newtonsoft.Json.Linq;

namespace COMP266EyeMaxLib.Logic
{
    // Provides functionality for fetching movie poster images from
    // The Movie Database (TMDB) API based on a movie title and desired size.
    public class ImageService
    {
        private const string apiKey = "50f4799410a1c57dc37e5d0e37b02f7d";
        private const string baseURL = "https://api.themoviedb.org/3";
        private const string imageBaseURL = "https://image.tmdb.org/t/p/w92";
        private const string largeImageBaseURL = "https://image.tmdb.org/t/p/w342";
        private static readonly HttpClient client = new HttpClient();

        // Searches the TMDB API for a movie poster by title and returns the image URL
        // at the specified size. Falls back to a local placeholder image if the movie
        // is not found or an error occurs.
        // param title - The title of the movie to search for on TMDB
        // param size - The desired poster size
        public async Task<string> getPosterImage(string title, string size)
        {
            string url = $"{baseURL}/search/movie?api_key={apiKey}&query={title}&include_adult=false&language=en-US&page=1";
            
            // https://api.themoviedb.org/3/search/movie?api_key=50f4799410a1c57dc37e5d0e37b02f7d&query=theGod&include_adult=false&language=en-US&page=1

            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            request.Headers.Add("Accept", "application/json");

            try
            {
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                
                var payload = await response.Content.ReadAsStringAsync();
                
                var results = JObject.Parse(payload)["results"];

                if (!results.HasValues)
                {
                    return "~/Images/no-image.png";
                }
                
                var image = results[0]["poster_path"].ToString();

                switch (size)
                {
                    case "small":
                        return $"{imageBaseURL}{image}";
                    case "large":
                        return $"{largeImageBaseURL}{image}";
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return "~/Images/no-image.png";
            }

            return "~/Images/no-image.png";
        }
    }
}
