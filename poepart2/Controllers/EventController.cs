using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using poepart2.Model;

namespace poepart2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly string connectionString =
        @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";


    // GET: api/Event
    // Get all events
    [HttpGet]
        public string getAllEvents()
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query = "SELECT * FROM Event";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                using SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int id = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string location = reader.GetString(2);
                    string date = reader.GetValue(3).ToString();

                    message +=
                        $"ID: {id}, Name: {name}, Location: {location}, Date: {date}\n";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // GET: api/Event/1
        // Get event by ID
        [HttpGet("{id}")]
        public string getEventById([FromRoute] int id)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "SELECT * FROM Event WHERE eventId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", id);

                using SqlDataReader reader = command.ExecuteReader();

                if (reader.Read())
                {
                    int eventId = reader.GetInt32(0);
                    string name = reader.GetString(1);
                    string location = reader.GetString(2);
                    string date = reader.GetValue(3).ToString();

                    message =
                        $"ID: {eventId}, Name: {name}, Location: {location}, Date: {date}";
                }
                else
                {
                    message = "Event not found.";
                }
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // POST: api/Event
        // Add event
        [HttpPost]
        public string addEvent([FromBody] EventModel newEvent)
        {
            if (newEvent == null)
            {
                return "Invalid event details.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "INSERT INTO Event (eventName, eventLocation, eventDate) " +
                    "VALUES (@name, @location, @date)";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@name", newEvent.EventName);

                command.Parameters.AddWithValue(
                    "@location", newEvent.EventLocation);

                command.Parameters.AddWithValue(
                    "@date", newEvent.EventDate);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Event added successfully."
                    : "Failed to add event.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // PUT: api/Event/1
        // Update event by ID using the ID in the URL
        [HttpPut("{id}")]
        public string updateEvent(
            [FromRoute] int id,
            [FromBody] EventModel updatedEvent)
        {
            if (updatedEvent == null)
            {
                return "Invalid event details.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "UPDATE Event SET " +
                    "eventName = @name, " +
                    "eventLocation = @location, " +
                    "eventDate = @date " +
                    "WHERE eventId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@id", id);

                command.Parameters.AddWithValue(
                    "@name", updatedEvent.EventName);

                command.Parameters.AddWithValue(
                    "@location", updatedEvent.EventLocation);

                command.Parameters.AddWithValue(
                    "@date", updatedEvent.EventDate);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Event updated successfully."
                    : "Event not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // PUT: api/Event
        // Update event using the ID in the request body
        [HttpPut]
        public string updateEventByBody(
            [FromBody] EventModel updatedEvent)
        {
            if (updatedEvent == null)
            {
                return "Invalid event details.";
            }

            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "UPDATE Event SET " +
                    "eventName = @name, " +
                    "eventLocation = @location, " +
                    "eventDate = @date " +
                    "WHERE eventId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@id", updatedEvent.ÉventId);

                command.Parameters.AddWithValue(
                    "@name", updatedEvent.EventName);

                command.Parameters.AddWithValue(
                    "@location", updatedEvent.EventLocation);

                command.Parameters.AddWithValue(
                    "@date", updatedEvent.EventDate);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Event updated successfully."
                    : "Event not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }

        // DELETE: api/Event/1
        // Delete event by ID
        [HttpDelete("{eventId}")]
        public string deleteEvent([FromRoute] int eventId)
        {
            string message = "";

            try
            {
                using SqlConnection connection =
                    new SqlConnection(connectionString);

                connection.Open();

                string query =
                    "DELETE FROM Event WHERE eventId = @id";

                using SqlCommand command =
                    new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@id", eventId);

                int rowsAffected = command.ExecuteNonQuery();

                message = rowsAffected > 0
                    ? "Event deleted successfully."
                    : "Event not found.";
            }
            catch (Exception error)
            {
                message = error.Message;
            }

            return message;
        }
    }


}
