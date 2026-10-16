using Microsoft.AspNetCore.Identity;

namespace poepart2.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<string> passwordHasher = new();

        public string HashPassword(string password)
        {
            return passwordHasher.HashPassword("poepart2-user", password);
        }

        public bool VerifyPassword(string hashedPassword, string suppliedPassword)
        {
            var result = passwordHasher.VerifyHashedPassword(
                "poepart2-user",
                hashedPassword,
                suppliedPassword
            );

            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}