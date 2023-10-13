using Microsoft.AspNetCore.Identity;
using GameAPI.Models;

namespace GameAPI.Services
{
    public class PasswordService
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public PasswordService()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public string HashPassword(string password)
        {
            return _passwordHasher.HashPassword(null, password);
        }

        public PasswordVerificationResult VerifyPassword(string hashedPassword, string providedPassword)
        {
            return _passwordHasher.VerifyHashedPassword(null, hashedPassword, providedPassword);
        }
    }
}

