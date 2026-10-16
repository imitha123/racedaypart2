
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using poepart2.Services;

namespace testingPOE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizerController : ControllerBase
    {
        private readonly string connectionString =
            @"Data Source=(localdb)\api_endpoints;Database=raceday;Trusted_Connection=True;";

        private readonly PasswordService passwordService;
        private readonly IConfiguration configuration;

        public OrganizerController(
            PasswordService passwordService,
            IConfiguration configuration)
        {
            this.passwordService = passwordService;
            this.configuration = configuration;
        }

        private bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   email.EndsWith(
                       "@gmail.com",
                       StringComparison.OrdinalIgnoreCase);
        }

        private bool IsValidPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                return false;

            return password.Length >= 8 &&
                   password.Any(char.IsUpper) &&
                   password.Any(char.IsLower) &&
                   password.Any(char.IsDigit) &&
                   password.Any(c => !char.IsLetterOrDigit(c));
        }

        private string GenerateJwtToken(int organizerId, string email)
        {
            string? jwtKey = configuration["Jwt:Key"];
            string? jwtIssuer = configuration["Jwt:Issuer"];
            string? jwtAudience = configuration["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(jwtKey) ||
                string.IsNullOrWhiteSpace(jwtIssuer) ||
                string.IsNullOrWhiteSpace(jwtAudience))
            {
                throw new InvalidOperationException(
                    "JWT configuration is missing.");
            }

            byte[] keyBytes;

            try
            {
                keyBytes = Convert.FromBase64String(jwtKey);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException(
                    "JWT key must be Base64 encoded.");
            }

            if (keyBytes.Length < 32)
            {
                throw new InvalidOperationException(
                    "JWT key must contain at least 32 bytes.");
            }

            var key = new SymmetricSecurityKey(keyBytes);

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(
                    ClaimTypes.NameIdentifier,
                    organizerId.ToString()),

                new Claim(
                    ClaimTypes.Email,
                    email),

                new Claim(
                    ClaimTypes.Role,
                    "Organizer")
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // GET: api/Organizer
        // Requires authentication.
        [HttpGet]
        [Authorize(Roles = "Organizer")]
        public IActionResult GetOrganizers()
        {
            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                const string query =
                    "SELECT organizerID, organizerName, organizerEmail, surname " +
                    "FROM Organizer";

                using var command = new SqlCommand(query, connection);
                using var reader = command.ExecuteReader();

                var organizers = new System.Collections.Generic.List<object>();

                while (reader.Read())
                {
                    organizers.Add(new
                    {
                        OrganizerID = reader.GetInt32(0),
                        OrganizerName = reader.GetString(1).Trim(),
                        OrganizerEmail = reader.GetString(2).Trim(),
                        Surname = reader.GetString(3).Trim()
                    });
                }

                return Ok(organizers);
            }
            catch (SqlException)
            {
                return StatusCode(
                    500,
                    new { message = "A database error occurred." });
            }
        }

        // POST: api/Organizer/Register
        [HttpPost("Register")]
        [AllowAnonymous]
        public IActionResult Register(
            [FromBody] poepart2.Model.OrganizerRegisterModel model)
        {
            if (model == null)
                return BadRequest(new { message = "Registration details are required." });

            if (string.IsNullOrWhiteSpace(model.Organizername) ||
                string.IsNullOrWhiteSpace(model.OrganizerSurname))
            {
                return BadRequest(new { message = "Name and surname are required." });
            }

            if (!IsValidEmail(model.OrganizerEmail))
            {
                return BadRequest(new
                {
                    message = "Email must end with @gmail.com."
                });
            }

            if (!IsValidPassword(model.Password))
            {
                return BadRequest(new
                {
                    message = "Password must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character."
                });
            }

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                const string checkQuery =
                    "SELECT COUNT(*) FROM Organizer WHERE LTRIM(RTRIM(organizerEmail)) = @email";

                using (var checkCommand = new SqlCommand(checkQuery, connection))
                {
                    checkCommand.Parameters.AddWithValue(
                        "@email",
                        model.OrganizerEmail.Trim());

                    int existing = (int)checkCommand.ExecuteScalar()!;

                    if (existing > 0)
                    {
                        return Conflict(new
                        {
                            message = "An organizer with this email already exists."
                        });
                    }
                }

                string hashedPassword =
                    passwordService.HashPassword(model.Password);

                const string insertQuery =
                    "INSERT INTO Organizer " +
                    "(organizerName, organizerEmail, password, surname) " +
                    "VALUES (@name, @email, @password, @surname)";

                using var command = new SqlCommand(insertQuery, connection);

                command.Parameters.AddWithValue(
                    "@name",
                    model.Organizername.Trim());

                command.Parameters.AddWithValue(
                    "@email",
                    model.OrganizerEmail.Trim());

                command.Parameters.AddWithValue(
                    "@password",
                    hashedPassword);

                command.Parameters.AddWithValue(
                    "@surname",
                    model.OrganizerSurname.Trim());

                int rows = command.ExecuteNonQuery();

                if (rows > 0)
                {
                    return StatusCode(201, new
                    {
                        message = "Organizer registered successfully."
                    });
                }

                return StatusCode(
                    500,
                    new { message = "Organizer registration failed." });
            }
            catch (SqlException)
            {
                return StatusCode(
                    500,
                    new { message = "A database error occurred during registration." });
            }
        }

        // POST: api/Organizer/login
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login(
            [FromBody] poepart2.Model.OrganizerLoginModel model)
        {
            if (model == null ||
                string.IsNullOrWhiteSpace(model.Email) ||
                string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new
                {
                    message = "Email and password are required."
                });
            }

            if (!IsValidEmail(model.Email))
            {
                return BadRequest(new
                {
                    message = "Email must end with @gmail.com."
                });
            }

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                const string query =
                    "SELECT organizerID, organizerName, organizerEmail, password " +
                    "FROM Organizer " +
                    "WHERE LTRIM(RTRIM(organizerEmail)) = @email";

                using var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue(
                    "@email",
                    model.Email.Trim());

                using var reader = command.ExecuteReader();

                if (!reader.Read())
                {
                    return Unauthorized(new
                    {
                        message = "Invalid email or password."
                    });
                }

                int organizerId = reader.GetInt32(0);
                string organizerName = reader.GetString(1).Trim();
                string organizerEmail = reader.GetString(2).Trim();
                string storedPassword = reader.GetString(3).Trim();

                reader.Close();

                // Verify the submitted password against the stored hash.
                bool passwordMatches;

                try
                {
                    passwordMatches = passwordService.VerifyPassword(
                        storedPassword,
                        model.Password);
                }
                catch (FormatException)
                {
                    passwordMatches = false;
                }

                if (!passwordMatches)
                {
                    return Unauthorized(new
                    {
                        message = "Invalid email or password."
                    });
                }

                string token = GenerateJwtToken(
                    organizerId,
                    organizerEmail);

                return Ok(new
                {
                    message = "Login successful.",
                    organizerId,
                    organizerName,
                    organizerEmail,
                    token,
                    tokenType = "Bearer",
                    expiresIn = 7200
                });
            }
            catch (SqlException)
            {
                return StatusCode(
                    500,
                    new { message = "A database error occurred during login." });
            }
        }

        // PUT: api/Organizer/1
        // An organizer can update only their own account.
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult UpdateOrganizer(
            int id,
            [FromBody] poepart2.Model.OrganizerRegisterModel model)
        {
            string? authenticatedId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(authenticatedId, out int tokenOrganizerId) ||
                tokenOrganizerId != id)
            {
                return Forbid();
            }

            if (model == null)
                return BadRequest(new { message = "Update details are required." });

            if (string.IsNullOrWhiteSpace(model.Organizername) ||
                string.IsNullOrWhiteSpace(model.OrganizerSurname))
            {
                return BadRequest(new { message = "Name and surname are required." });
            }

            if (!IsValidEmail(model.OrganizerEmail))
            {
                return BadRequest(new
                {
                    message = "Email must end with @gmail.com."
                });
            }

            if (!IsValidPassword(model.Password))
            {
                return BadRequest(new
                {
                    message = "Password must contain at least 8 characters, an uppercase letter, a lowercase letter, a number, and a special character."
                });
            }

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                const string emailCheck =
                    "SELECT COUNT(*) FROM Organizer " +
                    "WHERE LTRIM(RTRIM(organizerEmail)) = @email " +
                    "AND organizerID <> @id";

                using (var check = new SqlCommand(emailCheck, connection))
                {
                    check.Parameters.AddWithValue("@email", model.OrganizerEmail.Trim());
                    check.Parameters.AddWithValue("@id", id);

                    if ((int)check.ExecuteScalar()! > 0)
                    {
                        return Conflict(new
                        {
                            message = "Another organizer already uses this email."
                        });
                    }
                }

                string hashedPassword =
                    passwordService.HashPassword(model.Password);

                const string query =
                    "UPDATE Organizer SET " +
                    "organizerName = @name, " +
                    "organizerEmail = @email, " +
                    "password = @password, " +
                    "surname = @surname " +
                    "WHERE organizerID = @id";

                using var command = new SqlCommand(query, connection);

                command.Parameters.AddWithValue("@name", model.Organizername.Trim());
                command.Parameters.AddWithValue("@email", model.OrganizerEmail.Trim());
                command.Parameters.AddWithValue("@password", hashedPassword);
                command.Parameters.AddWithValue("@surname", model.OrganizerSurname.Trim());
                command.Parameters.AddWithValue("@id", id);

                int rows = command.ExecuteNonQuery();

                if (rows == 0)
                    return NotFound(new { message = "Organizer not found." });

                return Ok(new { message = "Organizer updated successfully." });
            }
            catch (SqlException)
            {
                return StatusCode(
                    500,
                    new { message = "A database error occurred while updating the organizer." });
            }
        }

        // DELETE: api/Organizer/1
        // An organizer can delete only their own account.
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Organizer")]
        public IActionResult DeleteOrganizer(int id)
        {
            string? authenticatedId =
                User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!int.TryParse(authenticatedId, out int tokenOrganizerId) ||
                tokenOrganizerId != id)
            {
                return Forbid();
            }

            try
            {
                using var connection = new SqlConnection(connectionString);
                connection.Open();

                const string query =
                    "DELETE FROM Organizer WHERE organizerID = @id";

                using var command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@id", id);

                int rows = command.ExecuteNonQuery();

                if (rows == 0)
                    return NotFound(new { message = "Organizer not found." });

                return Ok(new { message = "Organizer deleted successfully." });
            }
            catch (SqlException)
            {
                return StatusCode(
                    500,
                    new { message = "A database error occurred while deleting the organizer." });
            }
        }
    }
}

