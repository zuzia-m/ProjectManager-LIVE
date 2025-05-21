using System.Security.Cryptography;
using System.Text;

namespace ProjectManager.Helpers
{
    public class PasswordHelper
    {
        public static (string Hash, string Salt) HashPassowrd(string password)
        {
            using (HMACSHA512 hmac = new HMACSHA512())
            {
                var salt = hmac.Key;
                var hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return (Convert.ToBase64String(hash), Convert.ToBase64String(salt));
            }
        }
    }
}