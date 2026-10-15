using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace testingPOE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly string connectionString =
        @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";


    // Validate email
    private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   email.EndsWith(
                       "@gmail.com",
                       StringComparison.OrdinalIgnoreCase);
        }

        // Validate password
        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(c => !char.IsLetterOrDigit(c));
        }

        // GET: api/Organizer
        // Get all organizers
        [HttpGet]
        public string getOrganizer()
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM Organizer";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string organizerName = reader.GetString(1);
                    string organizerEmail = reader.GetString(2);

                    message +=
                        $"ID: {id}, Name: {organizerName}, Email: {organizerEmail}\n";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // POST: api/Organizer
        // Add organizer
        [HttpPost]
        public string AddOrganizer(
            string organizerName,
            string organizerEmail,
            string password,
            string surname)
        {
            if (!IsValidEmail(organizerEmail))
            {
                return "Invalid email. Email must end with @gmail.com.";
            }

            if (!IsValidPassword(password))
            {
                return "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "INSERT INTO Organizer " +
                    "(Organizername, OrganizerEmail, Password, surname) " +
                    "VALUES (@organizerName, @organizerEmail, @password, @surname)";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@organizerName", organizerName);

                command.Parameters.AddWithValue(
                    "@organizerEmail", organizerEmail);

                // Demonstration only: use a password hash in real applications.
                command.Parameters.AddWithValue("@password", password);

                command.Parameters.AddWithValue("@surname", surname);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Organizer added successfully."
                    : "Organizer could not be added.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // GET: api/Organizer/login?email=...&password=...
        // Find organizer by email and password
        [HttpGet("login")]
        public string GetOrganizerByEmailAndPassword(
            [FromQuery] string email,
            [FromQuery] string password)
        {
            if (!IsValidEmail(email))
            {
                return "Invalid email. Email must end with @gmail.com.";
            }

            if (!IsValidPassword(password))
            {
                return "Invalid password format.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "SELECT id, Organizername, OrganizerEmail " +
                    "FROM Organizer " +
                    "WHERE OrganizerEmail = @email AND Password = @password";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@email", email);
                command.Parameters.AddWithValue("@password", password);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int organizerId = reader.GetInt32(0);
                    string organizerName = reader.GetString(1);
                    string organizerEmail = reader.GetString(2);

                    message =
                        $"Organizer found: ID: {organizerId}, Name: {organizerName}, Email: {organizerEmail}";
                }
                else
                {
                    message = "Invalid email or password.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // DELETE: api/Organizer?id=1
        // Delete organizer
        [HttpDelete]
        public string DeleteOrganizer(int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM Organizer WHERE id = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Organizer deleted successfully."
                    : "Organizer not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // PUT: api/Organizer?id=1&organizerName=...&organizerEmail=...&password=...
        // Update organizer
        [HttpPut]
        public string UpdateOrganizer(
            int id,
            string organizerName,
            string organizerEmail,
            string password)
        {
            if (!IsValidEmail(organizerEmail))
            {
                return "Invalid email. Email must end with @gmail.com.";
            }

            if (!IsValidPassword(password))
            {
                return "Invalid password. It must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "UPDATE Organizer SET " +
                    "Organizername = @organizerName, " +
                    "OrganizerEmail = @organizerEmail, " +
                    "Password = @password " +
                    "WHERE id = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@organizerName", organizerName);

                command.Parameters.AddWithValue(
                    "@organizerEmail", organizerEmail);

                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Organizer updated successfully."
                    : "Organizer not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }


}
