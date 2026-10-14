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
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM Results";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string position = reader.GetString(1);
                    int score = reader.GetInt32(2);

                    message +=
                        $"ID: {id}, Position: {position}, Score: {score}\n";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // GET: api/Result/1
        // Get result by ID
        [HttpGet("{id}")]
        public string getResult([FromRoute] int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "SELECT * FROM Results WHERE resultId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int resultId = reader.GetInt32(0);
                    string position = reader.GetString(1);
                    int score = reader.GetInt32(2);

                    message =
                        $"ID: {resultId}, Position: {position}, Score: {score}";
                }
                else
                {
                    message = "Result not found.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // PUT: api/Result/1
        // Update result by ID
        [HttpPut("{id}")]
        public string updateResult(
            [FromRoute] int id,
            [FromBody] ResultsModel updatedResult)
        {
            if (updatedResult == null)
            {
                return "Invalid result details.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "UPDATE Results " +
                    "SET Position_ = @position, score = @score " +
                    "WHERE resultID = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@position", updatedResult.Position);

                command.Parameters.AddWithValue(
                    "@score", updatedResult.Score);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Result updated successfully."
                    : "Result not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // DELETE: api/Result/1
        // Delete result by ID
        [HttpDelete("{id}")]
        public string deleteResult([FromRoute] int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "DELETE FROM Results WHERE resultID = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Result deleted successfully."
                    : "Result not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }

}
