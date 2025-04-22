using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace LocalEyesAPI.Helpers
{

    public class EncryptionHelper
    {
        private readonly IConfiguration _configuration;
        private string _key;
        private string _salt = "LaesoeSalt";

        public EncryptionHelper(IConfiguration configuration)
        {
            _configuration = configuration;

            _key = Encoding.UTF8.GetBytes(_configuration["API:Key"]).ToString();
        }

        public string EncryptKey()
        {
            string combined = _key + _salt;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));

                //Debug.WriteLine("Hash: " + Convert.ToBase64String(hashBytes));

                return Convert.ToBase64String(hashBytes);
            }
        }
    }
}
