using System.Security.Cryptography;

namespace Skipper.Extensions
{
    public static class StringExtension
    {
        public static byte[] ConvertToHash(this string value, byte[] salt)
        {
            CreatePasswordHash(value, out byte[] PasswordHash, salt);
            return PasswordHash;
        }
        
        private static void CreatePasswordHash(string password, out byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                hmac.Key = passwordSalt;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        public static bool TrustTo(this string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }

    }
}
