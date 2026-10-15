
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
        [HttpGet("{resultID}")]
        public string getResult([FromRoute] int resultID)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM Results WHERE resultID = @resultID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@resultID", resultID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                message ="Result ID:"+ reader.GetValue(0).ToString() + " Score:"
                                        + reader.GetValue(1).ToString() + " Position:"
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
        [HttpPost]
        public string addResult([FromBody] ResultsModel newResult)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO Results (Position_, Score, participantID) VALUES (@position, @score, @participantID)";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@position", newResult.Position);
                        command.Parameters.AddWithValue("@score", newResult.Score);
                        command.Parameters.AddWithValue("@participantID", newResult.participantId);

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
        [HttpPut("{resultID}")]
        public string updateResult([FromRoute] int resultID, [FromBody] ResultsModel updatedResult)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE Results SET Position_ = @position, Score = @score WHERE resultID = @resultID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@position", updatedResult.Position);
                        command.Parameters.AddWithValue("@score", updatedResult.Score);
                        command.Parameters.AddWithValue("@resultID", resultID);

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
        [HttpDelete("{resultID}")]
        public string deleteResult([FromRoute] int resultID)
        {
            string message = "";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM Results WHERE resultID = @resultID";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@resultID", resultID);

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

