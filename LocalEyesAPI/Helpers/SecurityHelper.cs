using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.Cryptography;
using System.Text;

namespace LocalEyesAPI.Helpers
{

    public class SecurityHelper
    {
        private readonly IConfiguration _configuration;
        private string _key;
        private string _salt = "LaesoeSalt";

        public SecurityHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            _key = Encoding.UTF8.GetBytes(_configuration["BasicAuth:Key"]).ToString();
        }

        public string EncryptKey()
        {
            string combined = _key + _salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));

                return Convert.ToBase64String(hashBytes);
            }
        }

        public string CompareKeys(string localKey, string receivedKey)
        {
            string combined = _key + _salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
