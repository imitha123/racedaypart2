using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using poepart2.Model;
using Microsoft.Data.SqlClient;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        
        // Get all events
        [HttpGet]
        public string getAllEvents()
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Event";

                SqlCommand command = new SqlCommand(query, connection);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string location = reader.GetString(2);
                    string date = reader.GetString(3);

                    message += $"ID: {id}, Name: {name}, Location: {location}, Date: {date}\n";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Update event by id
        [HttpPut("{id}")]
        public string updateEvent(int eventId, Model.EventModel updatedEvent)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE Event SET Name = @name, Location = @location, Date = @date WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", eventId);
                command.Parameters.AddWithValue("@name", updatedEvent.EventName);
                command.Parameters.AddWithValue("@location", updatedEvent.EventLocation);
                command.Parameters.AddWithValue("@date", updatedEvent.EventDate);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Event updated successfully.";
                }
                else
                {
                    message = "Failed to update event.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Get Event By ID
        [HttpGet("{id}")]
        public string getEventById(int eventId)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "SELECT * FROM Event WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", eventId);

                SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string location = reader.GetString(2);
                    string date = reader.GetString(3);

                    message = $"ID: {id}, Name: {name}, Location: {location}, Date: {date}";
                }
                else
                {
                    message = "Event not found.";
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


        // Add event
        [HttpPost]
        public string addEvent(Model. EventModel newEvent)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "INSERT INTO Event (Name, Location, Date) VALUES (@name, @location, @date)";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", newEvent.EventName);
                command.Parameters.AddWithValue("@location", newEvent.EventLocation);
                command.Parameters.AddWithValue("@date", newEvent.EventDate);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Event added successfully.";
                }
                else
                {
                    message = "Failed to add event.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Update Event
        [HttpPut]
        public string updateEvent([FromBody] EventModel updatedEvent)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "UPDATE Event SET Name = @name, Location = @location, Date = @date WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", updatedEvent.ÉventId);
                command.Parameters.AddWithValue("@name", updatedEvent.EventName);
                command.Parameters.AddWithValue("@location", updatedEvent.EventLocation);
                command.Parameters.AddWithValue("@date", updatedEvent.EventDate);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Event updated successfully.";
                }
                else
                {
                    message = "Failed to update event.";
                }

                connection.Close();
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }


        // Delete event
        [HttpDelete]
        public string deleteEvent(int eventId)
        {
            string message = "";

            try
            {
                string connectionString =
                    @"Data Source=(localhost)\api_endpoints;Database=raceday;Trusted_Connection=True;";

                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = "DELETE FROM Event WHERE Id = @id";

                SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", eventId);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    message = "Event deleted successfully.";
                }
                else
                {
                    message = "Failed to delete event.";
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