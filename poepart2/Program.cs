using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using poepart2.Services;

namespace poepart2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


        // Add controllers.
        builder.Services.AddControllers();

            // Add Swagger.
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Read JWT configuration.
            string? jwtKey = builder.Configuration["Jwt:Key"];
            string? jwtIssuer = builder.Configuration["Jwt:Issuer"];
            string? jwtAudience = builder.Configuration["Jwt:Audience"];

            // Prevent the application from starting with missing JWT settings.
            if (string.IsNullOrWhiteSpace(jwtKey) ||
                string.IsNullOrWhiteSpace(jwtIssuer) ||
                string.IsNullOrWhiteSpace(jwtAudience))
            {
                throw new InvalidOperationException(
                    "JWT configuration is missing. Check Jwt:Key, Jwt:Issuer and Jwt:Audience.");
            }

            byte[] signingKey;

            try
            {
                signingKey = Convert.FromBase64String(jwtKey);
            }
            catch (FormatException)
            {
                throw new InvalidOperationException(
                    "Jwt:Key must contain a valid Base64-encoded secret.");
            }

            if (signingKey.Length < 32)
            {
                throw new InvalidOperationException(
                    "Jwt:Key must decode to at least 32 bytes.");
            }

            // Configure JWT authentication.
            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidIssuer = jwtIssuer,

                            ValidateAudience = true,
                            ValidAudience = jwtAudience,

                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey =
                                new SymmetricSecurityKey(signingKey),

                            ValidateLifetime = true,
                            ClockSkew = TimeSpan.FromSeconds(30)
                        };
                });

            // Enable role-based authorization.
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<PasswordService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI();

            // Authentication must run before authorization.
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }


}
