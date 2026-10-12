
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using poepart2.Model;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {
        private readonly string connectionString =
            @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

        // Validate email address
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

        // Add participant
        [HttpPost]
        public string AddParticipant(
            string participantName,
            string participantSurname,
            string email,
            string password)
        {
            if (!IsValidEmail(email))
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
                    "INSERT INTO Participant " +
                    "(Participantname, Participantsurname, Email, Password) " +
                    "VALUES (@name, @surname, @email, @password)";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", participantName);
                command.Parameters.AddWithValue("@surname", participantSurname);
                command.Parameters.AddWithValue("@email", email);

                // For a real application, store a password hash instead.
                command.Parameters.AddWithValue("@password", password);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Participant added successfully!"
                    : "Participant could not be added.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // Get participant by email
        [HttpGet("{participantEmail}")]
        public string GetParticipant(string email)
        {
            if (!IsValidEmail(email))
            {
                return "Invalid email. Email must end with @gmail.com.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "SELECT Participantname, Participantsurname, Email " +
                    "FROM Participant WHERE Email = @email";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@email", email);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    string name = reader.GetString(0);
                    string surname = reader.GetString(1);
                    string participantEmail = reader.GetString(2);

                    message =
                        $"Participant found: {name} {surname}, Email: {participantEmail}";
                }
                else
                {
                    message = "Participant not found.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // Update participant
        [HttpPut("{id}")]
        public string UpdateParticipant(
            int id,
            [FromBody] ParticipantModel updatedParticipant)
        {
            if (updatedParticipant == null)
            {
                return "Invalid participant details.";
            }

            if (!IsValidEmail(updatedParticipant.participantEmail))
            {
                return "Invalid email. Email must end with @gmail.com.";
            }

            if (!IsValidPassword(updatedParticipant.participantPassword))
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
                    "UPDATE Participant SET " +
                    "Participantname = @name, " +
                    "Participantsurname = @surname, " +
                    "Email = @email, " +
                    "Password = @password " +
                    "WHERE id = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@name", updatedParticipant.Participantname);

                command.Parameters.AddWithValue(
                    "@surname", updatedParticipant.Participantsurname);

                command.Parameters.AddWithValue(
                    "@email", updatedParticipant.participantEmail);

                // For a real application, store a password hash instead.
                command.Parameters.AddWithValue(
                    "@password", updatedParticipant.participantPassword);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Participant updated successfully!"
                    : "Participant not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // Delete participant
        [HttpDelete("{id}")]
        public string DeleteParticipant(int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "DELETE FROM Participant WHERE id = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Participant deleted successfully!"
                    : "Participant not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }
}

