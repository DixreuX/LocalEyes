using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace LocalEyes.Shared.Helpers
{
    public class EncryptionHelper
    {
        private string _key;
        private string _salt = "LaesoeSalt";

        public EncryptionHelper(string apiKey)
        {
            _key = Encoding.UTF8.GetBytes(apiKey).ToString();
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
    }
}
