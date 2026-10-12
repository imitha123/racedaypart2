using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poepart2.Model;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ResultController : ControllerBase
    {

        // Get all results
        [HttpGet]
        public string getAllResults()
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Results";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string position = reader.GetString(1);
                    int score = reader.GetInt32(2);

                    message += $"ID: {id}, Position: {position}, Score: {score}\n";
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Get result by id
        [HttpGet("{id}")]
        public string getResult(int id)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Results WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int resultId = reader.GetInt32(0);
                    string position = reader.GetString(1);
                    int score = reader.GetInt32(2);

                    message = $"ID: {resultId}, Position: {position}, Score: {score}";
                }
                else
                {
                    message = "Result not found.";
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Update result
        [HttpPut("{id}")]
        public string updateResult(int id,  Model.ResultsModel updatedResult)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE Results " +
                               "SET Position = @position, Score = @score " +
                               "WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@position", updatedResult.Position);
                command.Parameters.AddWithValue("@score", updatedResult.Score);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Result updated successfully.";
                }
                else
                {
                    message = "Result not found.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Delete result by id
        [HttpDelete("{id}")]
        public string deleteResult(int id)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM Results WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Result deleted successfully.";
                }
                else
                {
                    message = "Result not found.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

    }
}