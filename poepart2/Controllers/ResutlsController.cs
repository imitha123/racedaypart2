
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using poepart2.Model;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {
        private readonly string connectionString =
            @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

        // GET: api/Result
        // Get all results
        [HttpGet]
        public string getAllResults()
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Results";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            message += reader.GetValue(0).ToString() + " "
                                    + reader.GetValue(1).ToString() + " "
                                    + reader.GetValue(2).ToString() + "\n";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error retrieving results: " + ex.Message;
            }

            return message;
        }

        // GET: api/Result/1
        // Get a result by ID
        [HttpGet("{id}")]
        public string getResult([FromRoute] int id)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Results WHERE Id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                message = reader.GetValue(0).ToString() + " "
                                        + reader.GetValue(1).ToString() + " "
                                        + reader.GetValue(2).ToString();
                            }
                            else
                            {
                                message = "Result not found.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error retrieving result: " + ex.Message;
            }

            return message;
        }

        // POST: api/Result
        // Add a new result
        [HttpPost]
        public string addResult([FromBody] ResultsModel newResult)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Results (Position, Score) VALUES (@position, @score)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@position", newResult.Position);
                        command.Parameters.AddWithValue("@score", newResult.Score);

                        int rowsAffected = command.ExecuteNonQuery();

                        message = rowsAffected > 0
                            ? "Result added successfully."
                            : "Failed to add result.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error adding result: " + ex.Message;
            }

            return message;
        }

        // PUT: api/Result/1
        // Update an existing result
        [HttpPut("{id}")]
        public string updateResult([FromRoute] int id, [FromBody] ResultsModel updatedResult)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Results SET Position = @position, Score = @score WHERE Id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@position", updatedResult.Position);
                        command.Parameters.AddWithValue("@score", updatedResult.Score);
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        message = rowsAffected > 0
                            ? "Result updated successfully."
                            : "Result not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error updating result: " + ex.Message;
            }

            return message;
        }

        // DELETE: api/Result/1
        // Delete a result by ID
        [HttpDelete("{id}")]
        public string deleteResult([FromRoute] int id)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Results WHERE Id = @id";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        int rowsAffected = command.ExecuteNonQuery();

                        message = rowsAffected > 0
                            ? "Result deleted successfully."
                            : "Result not found.";
                    }
                }
            }
            catch (Exception ex)
            {
                message = "Error deleting result: " + ex.Message;
            }

            return message;
        }
    }
}

