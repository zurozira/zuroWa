using COMP266EyeMaxLib.Domain;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using COMP266EyeMaxLib.Data;

namespace COMP266EyeMaxLib.Data
{
    // Provides data access methods for retrieving genre (category) records
    // from the database.
    internal class GenreRepository
    {
        // Retrieves all genre records from the Categories table.
        // returns A List of Genre objects representing all rows in the Categories table.
        public List<Genre> SelectAll()
        {
            List<Genre> genres = new List<Genre>();

            SqlConnection con = new SqlConnection(ConnectionBuilder.ConnectionString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM Categories";

            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    genres.Add(
                        new Genre(
                            (int)reader["CategoryID"],
                            (string)reader["Description"]
                        ));
                }
                con.Close();
            }

            return genres;
        }

        // Retrieves a single genre record from the Categories table
        // that matches the specified ID.
        // param id - The unique identifier (CategoryId) of the genre to retrieve.
        // return A Genre object populated with the matching data,
        public Genre SelectOne(int id)
        {
            Genre genre = new Genre();
            SqlConnection con = new SqlConnection(ConnectionBuilder.ConnectionString());
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            cmd.CommandText = "SELECT * FROM Categories WHERE CategoryId=@Id";
            cmd.Parameters.AddWithValue("Id", id);

            using (con)
            {
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    genre = new Genre(
                            (int)reader["CategoryID"],
                            (string)reader["Description"]
                            );
                }
                con.Close();
            }

            return genre;
        }
    }
}
