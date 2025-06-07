using SalesPilotCRM.Application.Interfaces.Auth;
using System.Security.Cryptography;
using System.Text;

namespace SalesPilotCRM.Infrastructure.Hashing
{
    public class PasswordHasher : IPasswordHasher
    {
        public (byte[] PasswordHash, byte[] PasswordSalt) HashPassword(string password)
        {
            using var hmac = new HMACSHA512();
            var salt = hmac.Key;
            var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return (hash, salt);
        }

        public bool VerifyPassword(string password, byte[] storedHash, byte[] storedSalt)
        {
            using var hmac = new HMACSHA512(storedSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(storedHash);
        }
    }
}
