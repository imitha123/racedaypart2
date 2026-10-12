using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParticipantController : ControllerBase
    {

        // Add Participant
        [HttpPost]
        public string AddParticipant(
            string participantName,
            string participantSurname
          )
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "INSERT INTO Participant " +
                               "(Participantname, Participantsurname) " +
                               "VALUES (@participantName, @participantSurname)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@participantName", participantName);
                command.Parameters.AddWithValue("@participantSurname", participantSurname);
                

                command.ExecuteNonQuery();

                message = "Participant added successfully!";

                connection.Close();
            }
            catch (Exception E)
            {
                message = "Error: " + E.Message;
            }

            return message;
        }


        // Get Participant
        [HttpGet("{id}")]
        public string GetParticipant(string participantName, string participantSurname)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM Participant " +
                               "WHERE Participantname = @participantName " +
                               "AND Participantsurname = @participantSurname";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@participantName", participantName);
                command.Parameters.AddWithValue("@participantSurname", participantSurname);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        message = "Participant found: " +
                                  reader["Participantname"] + " " +
                                  reader["Participantsurname"];
                    }
                }
                else
                {
                    message = "Participant not found.";
                }

                reader.Close();
                connection.Close();
            }
            catch (Exception E)
            {
                message = "Error: " + E.Message;
            }

            return message;
        }


        // Update Participant
        [HttpPut("{id}")]
        public string UpdateParticipant(int id, Model.ParticipantModel updatedParticipant)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "UPDATE Participant SET " +
                               "Participantname = @participantName, " +
                               "Participantsurname = @participantSurname " +
                               "WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@participantName",
                    updatedParticipant.Participantname);

                command.Parameters.AddWithValue(
                    "@participantSurname",
                    updatedParticipant.Participantsurname);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Participant updated successfully!";
                }
                else
                {
                    message = "Participant not found.";
                }

                connection.Close();
            }
            catch (Exception E)
            {
                message = "Error: " + E.Message;
            }

            return message;
        }


        // Delete Participant
        [HttpDelete("{id}")]
        public string DeleteParticipant(int id)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);

                connection.Open();

                string query = "DELETE FROM Participant WHERE id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Participant deleted successfully!";
                }
                else
                {
                    message = "Participant not found.";
                }

                connection.Close();
            }
            catch (Exception E)
            {
                message = "Error: " + E.Message;
            }

            return message;
        }

        

    }
}