using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace testingPOE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {

      


        // GET: api/Organizer
        [HttpGet]
        public string getOrganizer()
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Organizer";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string organizerName = reader.GetString(1);
                    string organizerEmail = reader.GetString(2);

                    message += $"ID: {id}, Name: {organizerName}, Email: {organizerEmail}\n";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // POST: api/Organizer
        [HttpPost]
        public string AddOrganizer(string organizerName, string organizerEmail)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO Organizer (Organizername, OrganizerEmail) VALUES (@organizerName, @organizerEmail)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@organizerName", organizerName);
                command.Parameters.AddWithValue("@organizerEmail", organizerEmail);

                command.ExecuteNonQuery();

                connection.Close();

                message = "Organizer added successfully.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // GET: api/Organizer/{id}
        [HttpGet("{id}")]
        public string GetOrganizerById(int id)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Organizer WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int organizerId = reader.GetInt32(0);
                    string organizerName = reader.GetString(1);
                    string organizerEmail = reader.GetString(2);

                    message = $"ID: {organizerId}, Name: {organizerName}, Email: {organizerEmail}";
                }
                else
                {
                    message = "Organizer not found.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // DELETE: api/Organizer/{id}
        [HttpDelete("{id}")]
        public string DeleteOrganizer(int id)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM Organizer WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Organizer deleted successfully.";
                }
                else
                {
                    message = "Organizer not found.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // UPDATE: api/Organizer/{id}
        [HttpPut("{id}")]
        public string UpdateOrganizer(int id, string organizerName, string organizerEmail)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE Organizer SET Organizername = @organizerName, OrganizerEmail = @organizerEmail WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@organizerName", organizerName);
                command.Parameters.AddWithValue("@organizerEmail", organizerEmail);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Organizer updated successfully.";
                }
                else
                {
                    message = "Organizer not found.";
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