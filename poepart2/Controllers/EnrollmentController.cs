using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {
        private readonly string connectionString =
        @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";


    // GET: api/Enrollment
    // Get all enrollments
    [HttpGet]
        public string getAllEnrollments()
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM Enrollment";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int eventId = reader.GetInt32(0);
                    int participantId = reader.GetInt32(1);

                    message +=
                        $"EventID: {eventId}, ParticipantID: {participantId}\n";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // GET: api/Enrollment/1/2
        // Get enrollment by event ID and participant ID
        [HttpGet("{eventId}/{participantId}")]
        public string getEnrollment(
            [FromRoute] int eventId,
            [FromRoute] int participantId)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "SELECT * FROM Enrollment " +
                    "WHERE EventID = @eventId AND ParticipantID = @participantId";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int enrollmentEventId = reader.GetInt32(0);
                    int enrollmentParticipantId = reader.GetInt32(1);

                    message =
                        $"EventID: {enrollmentEventId}, ParticipantID: {enrollmentParticipantId}";
                }
                else
                {
                    message = "Enrollment not found.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // POST: api/Enrollment?eventId=1&participantId=2
        // Add enrollment
        [HttpPost]
        public string addEnrollment(
            [FromQuery] int eventId,
            [FromQuery] int participantId)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "INSERT INTO Enrollment (EventID, ParticipantID) " +
                    "VALUES (@eventId, @participantId)";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Enrollment added successfully."
                    : "Enrollment could not be added.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // PUT: api/Enrollment/1/2?newEventId=3&newParticipantId=4
        // Update enrollment IDs
        [HttpPut("{eventId}/{participantId}")]
        public string updateEnrollment(
            [FromRoute] int eventId,
            [FromRoute] int participantId,
            [FromQuery] int newEventId,
            [FromQuery] int newParticipantId)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "UPDATE Enrollment " +
                    "SET EventID = @newEventId, ParticipantID = @newParticipantId " +
                    "WHERE EventID = @eventId AND ParticipantID = @participantId";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);
                command.Parameters.AddWithValue("@newEventId", newEventId);
                command.Parameters.AddWithValue("@newParticipantId", newParticipantId);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Enrollment updated successfully."
                    : "Enrollment not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // DELETE: api/Enrollment/1/2
        // Delete enrollment by event ID and participant ID
        [HttpDelete("{eventId}/{participantId}")]
        public string deleteEnrollment(
            [FromRoute] int eventId,
            [FromRoute] int participantId)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "DELETE FROM Enrollment " +
                    "WHERE EventID = @eventId AND ParticipantID = @participantId";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Enrollment deleted successfully."
                    : "Enrollment not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }

}
