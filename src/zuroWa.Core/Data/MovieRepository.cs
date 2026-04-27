using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;
using COMP266EyeMaxLib.Domain;
using COMP266EyeMaxLib.Data;

namespace COMP266EyeMaxLib.Data
{
    // Provides data access methods for performing CRUD operations on Movie records in the database
    internal class MovieRepository
    {
        private readonly SqlConnection con = new SqlConnection(ConnectionBuilder.ConnectionString());
        private readonly SqlCommand cmd = new SqlCommand();

        // Retrieves all movie records from the Movies table.
        // returns A List of Movie objects representing all rows in the Movies table.
        public List<Movie> SelectAll()
        {
            List<Movie> movies = new List<Movie>();

            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movies";

            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    movies.Add(
                        new Movie(
                            (int)reader["Id"],
                            (string)reader["Title"],
                            (string)reader["Director"],
                            (DateTime)reader["DateReleased"],
                            (string)reader["Description"],
                            (int)reader["CategoryId"],
                            (bool)reader["InTheaters"]
                        ));
                }
                con.Close();
            }
            return movies;
        }

        // Retrieves a single movie record from the Movies table that matches the specified ID.
        // param id - The unique identifier of the movie to retrieve.
        // returns A Movie object populated with the matching record's data,
        public Movie SelectOne(int id)
        {
            Movie m = new Movie();

            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movies WHERE Id=@id";
            
            cmd.Parameters.AddWithValue("@id", id);

            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    m =
                        new Movie(
                            (int)reader["Id"],
                            (string)reader["Title"],
                            (string)reader["Director"],
                            (DateTime)reader["DateReleased"],
                            (string)reader["Description"],
                            (int)reader["CategoryId"],
                            (bool)reader["InTheaters"]
                        );
                }
                con.Close();
            }

            return m;
        }

        // Retrieves all movies that are currently showing in theaters.
        // returns A List of Movie objects where InTheaters is true
        public List<Movie> GetNowShowing()
        {
            List<Movie> nowShowingMovies = new List<Movie>();

            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM Movies WHERE InTheaters=1";

            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    nowShowingMovies.Add(
                        new Movie(
                            (int)reader["Id"],
                            (string)reader["Title"],
                            (string)reader["Director"],
                            (DateTime)reader["DateReleased"],
                            (string)reader["Description"],
                            (int)reader["CategoryId"],
                            (bool)reader["InTheaters"]
                        ));
                }
                con.Close();
            }
            return nowShowingMovies;
        }

        // Inserts a new movie record into the Movies table.
        // param newMovie - A Movie object containing the title, director, release date,
        // description, category ID, and theater status of the movie to add.
        public void AddMovie(Movie newMovie)
        {
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO Movies (Title, Director, DateReleased, Description, CategoryId, InTheaters) VALUES (@title, @director, @dateReleased, @description, @categoryId, @inTheaters)";

            cmd.Parameters.AddWithValue("@title", newMovie.Title);
            cmd.Parameters.AddWithValue("@director", newMovie.Director);
            cmd.Parameters.AddWithValue("@dateReleased", newMovie.DateReleased);
            cmd.Parameters.AddWithValue("@description", newMovie.Description);
            cmd.Parameters.AddWithValue("@categoryId", newMovie.CategoryId);
            cmd.Parameters.AddWithValue("@inTheaters", newMovie.InTheaters);

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        // Deletes the movie record with the specified ID from the Movies table.
        // param id - The unique identifier of the movie to delete
        public void DeleteMovie(int id)
        {
            cmd.Connection = con;
            cmd.CommandText = "DELETE Movies WHERE Id=@Id";

            cmd.Parameters.AddWithValue("Id", id);

            using (con)
            {
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }
    }
}
