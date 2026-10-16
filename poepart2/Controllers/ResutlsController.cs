using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
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
        [AllowAnonymous]
        public IActionResult GetAllResults()
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = @"
                SELECT resultID, Position_, Score, participantID
                FROM Results";

                using SqlCommand command = new SqlCommand(query, connection);
                using SqlDataReader reader = command.ExecuteReader();

                var results = new List<object>();

                while (reader.Read())
                {
                    results.Add(new
                    {
                        ResultID = reader.GetInt32(0),
                        Position = reader.GetValue(1),
                        Score = reader.GetValue(2),
                        ParticipantID = reader.GetInt32(3)
                    });
                }

                return Ok(results);
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    message = "A database error occurred while retrieving results."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while retrieving results."
                });
            }
        }

        // GET: api/Result/1
        // Get a result by ID
        [HttpGet("{resultID:int}")]
        [AllowAnonymous]
        public IActionResult GetResult(int resultID)
        {
            if (resultID <= 0)
            {
                return BadRequest(new
                {
                    message = "Result ID must be greater than zero."
                });
            }

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = @"
                SELECT resultID, Position_, Score, participantID
                FROM Results
                WHERE resultID = @resultID";

                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@resultID", resultID);

                using SqlDataReader reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    return NotFound(new
                    {
                        message = "Result not found."
                    });
                }

                var result = new
                {
                    ResultID = reader.GetInt32(0),
                    Position = reader.GetValue(1),
                    Score = reader.GetValue(2),
                    ParticipantID = reader.GetInt32(3)
                };

                return Ok(result);
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    message = "A database error occurred while retrieving the result."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while retrieving the result."
                });
            }
        }

        // POST: api/Result
        // Add a result
        [HttpPost]
        [Authorize(Roles = "Organizer")]
        public IActionResult AddResult([FromBody] ResultsModel newResult)
        {
            if (newResult == null)
            {
                return BadRequest(new
                {
                    message = "Result details are required."
                });
            }

            if (newResult.participantId <= 0)
            {
                return BadRequest(new
                {
                    message = "Participant ID must be greater than zero."
                });
            }

            if (string.IsNullOrWhiteSpace(newResult.Position) ||
!int.TryParse(newResult.Position, out int position) ||
position <= 0 ||
newResult.Score < 0)
            {
                return BadRequest(new
                {
                    message = "Position must be a positive number and score cannot be negative."
                });
            }


            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Check whether the participant exists.
                string participantQuery = @"
                SELECT COUNT(*)
                FROM Participant
                WHERE participantID = @participantID";

                using SqlCommand participantCommand =
                    new SqlCommand(participantQuery, connection);

                participantCommand.Parameters.AddWithValue(
                    "@participantID", newResult.participantId);

                int participantExists =
                    (int)participantCommand.ExecuteScalar()!;

                if (participantExists == 0)
                {
                    return BadRequest(new
                    {
                        message = "The specified participant does not exist."
                    });
                }

                string query = @"
                INSERT INTO Results (Position_, Score, participantID)
                VALUES (@position, @score, @participantID);

                SELECT CAST(SCOPE_IDENTITY() AS INT);";

                using SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@position", newResult.Position);
                command.Parameters.AddWithValue("@score", newResult.Score);
                command.Parameters.AddWithValue(
                    "@participantID", newResult.participantId);

                int resultID = (int)command.ExecuteScalar()!;

                return CreatedAtAction(
                    nameof(GetResult),
                    new { resultID },
                    new
                    {
                        ResultID = resultID,
                        Position = newResult.Position,
                        Score = newResult.Score,
                        ParticipantID = newResult.participantId,
                        Message = "Result added successfully."
                    });
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    message = "A database error occurred while adding the result."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while adding the result."
                });
            }
        }

        // PUT: api/Result/1
        // Update a result
        [HttpPut("{resultID:int}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult UpdateResult(
            int resultID,
            [FromBody] ResultsModel updatedResult)
        {
            if (resultID <= 0)
            {
                return BadRequest(new
                {
                    message = "Result ID must be greater than zero."
                });
            }

            if (updatedResult == null)
            {
                return BadRequest(new
                {
                    message = "Updated result details are required."
                });
            }

            if (updatedResult.participantId <= 0)
            {
                return BadRequest(new
                {
                    message = "Participant ID must be greater than zero."
                });
            }

            if (string.IsNullOrWhiteSpace(updatedResult.Position) ||
!int.TryParse(updatedResult.Position, out int position) ||
position <= 0 ||
updatedResult.Score < 0)
            {
                return BadRequest(new
                {
                    message = "Position must be a positive number and score cannot be negative."
                });
            }


            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Check that the result exists.
                string resultQuery = @"
                SELECT COUNT(*)
                FROM Results
                WHERE resultID = @resultID";

                using SqlCommand resultCommand =
                    new SqlCommand(resultQuery, connection);

                resultCommand.Parameters.AddWithValue("@resultID", resultID);

                if ((int)resultCommand.ExecuteScalar()! == 0)
                {
                    return NotFound(new
                    {
                        message = "Result not found."
                    });
                }

                // Check that the participant exists.
                string participantQuery = @"
                SELECT COUNT(*)
                FROM Participant
                WHERE participantID = @participantID";

                using SqlCommand participantCommand =
                    new SqlCommand(participantQuery, connection);

                participantCommand.Parameters.AddWithValue(
                    "@participantID", updatedResult.participantId);

                if ((int)participantCommand.ExecuteScalar()! == 0)
                {
                    return BadRequest(new
                    {
                        message = "The specified participant does not exist."
                    });
                }

                string query = @"
                UPDATE Results
                SET Position_ = @position,
                    Score = @score,
                    participantID = @participantID
                WHERE resultID = @resultID";

                using SqlCommand command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@position", updatedResult.Position);
                command.Parameters.AddWithValue("@score", updatedResult.Score);
                command.Parameters.AddWithValue(
                    "@participantID", updatedResult.participantId);
                command.Parameters.AddWithValue("@resultID", resultID);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound(new
                    {
                        message = "Result not found."
                    });
                }

                return Ok(new
                {
                    message = "Result updated successfully."
                });
            }
            catch (SqlException)
            {
                return StatusCode(500, new
                {
                    message = "A database error occurred while updating the result."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while updating the result."
                });
            }
        }

        // DELETE: api/Result/1
        // Delete a result
        [HttpDelete("{resultID:int}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult DeleteResult(int resultID)
        {
            if (resultID <= 0)
            {
                return BadRequest(new
                {
                    message = "Result ID must be greater than zero."
                });
            }

            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                string query = @"
                DELETE FROM Results
                WHERE resultID = @resultID";

                using SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@resultID", resultID);

                int rowsAffected = command.ExecuteNonQuery();

                if (rowsAffected == 0)
                {
                    return NotFound(new
                    {
                        message = "Result not found."
                    });
                }

                return Ok(new
                {
                    message = "Result deleted successfully."
                });
            }
            catch (SqlException)
            {
                return Conflict(new
                {
                    message = "The result could not be deleted because of a database constraint."
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred while deleting the result."
                });
            }
        }
    }


}
