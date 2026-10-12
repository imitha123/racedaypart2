using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poepart2.Model;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController : ControllerBase
    {

        // Get all enrollments
        [HttpGet]
        public string getAllEnrollments()
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Enrollment";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int eventId = reader.GetInt32(0);
                    int participantId = reader.GetInt32(1);

                    message += $"EventID: {eventId}, ParticipantID: {participantId}\n";
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


        // Delete enrollment by event id and participant id
        [HttpDelete("{eventId}/{participantId}")]
        public string deleteEnrollment(int eventId, int participantId)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query =
                    "DELETE FROM Enrollment WHERE EventID = @eventId AND ParticipantID = @participantId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Enrollment deleted successfully.";
                }
                else
                {
                    message = "Enrollment not found.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Get enrollment by event id and participant id
        [HttpGet("{eventId}/{participantId}")]
        public string getEnrollment(int eventId, int participantId)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query =
                    "SELECT * FROM Enrollment WHERE EventID = @eventId AND ParticipantID = @participantId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);

                SqlDataReader reader = command.ExecuteReader();

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

                reader.Close();
                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
        // Add enrollment
        [HttpPost]
        public string addEnrollment(int eventId, int participantId)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query =
                    "INSERT INTO Enrollment (EventID, ParticipantID) " +
                    "VALUES (@eventId, @participantId)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Enrollment added successfully.";
                }
                else
                {
                    message = "Enrollment could not be added.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
        // Update enrollment
        [HttpPut("{eventId}/{participantId}")]
        public string updateEnrollment(
            int eventId,
            int participantId,
            int newEventId,
            int newParticipantId)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query =
                    "UPDATE Enrollment " +
                    "SET EventID = @newEventId, ParticipantID = @newParticipantId " +
                    "WHERE EventID = @eventId AND ParticipantID = @participantId";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@eventId", eventId);
                command.Parameters.AddWithValue("@participantId", participantId);
                command.Parameters.AddWithValue("@newEventId", newEventId);
                command.Parameters.AddWithValue("@newParticipantId", newParticipantId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Enrollment updated successfully.";
                }
                else
                {
                    message = "Enrollment not found.";
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