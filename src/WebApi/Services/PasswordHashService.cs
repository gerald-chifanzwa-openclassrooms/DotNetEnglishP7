using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace WebApi.Services
{
    public class PasswordHashService : IPasswordHashService
    {
        private readonly IConfiguration _configuration;

        public PasswordHashService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Hash(string plainText)
        {
            // Use RijndaelManaged encryption

            // Seeding vector
            const string vector = "A3519D704D397195";

            // Get encryption key from Configuration
            var key = _configuration.GetSection("Security:EncryptionKey").Get<string>();
            var bytes1 = Encoding.UTF8.GetBytes(vector);
            var bytes2 = Encoding.UTF8.GetBytes(plainText);
            var bytes3 = new Rfc2898DeriveBytes(key, bytes1).GetBytes(32);
            var rijndaelManaged = new RijndaelManaged { Mode = CipherMode.CBC };
            var encryptor = rijndaelManaged.CreateEncryptor(bytes3, bytes1);
            var memoryStream = new MemoryStream();
            var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(bytes2, 0, bytes2.Length);
            cryptoStream.FlushFinalBlock();
            var array = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(array);
        }
    }
}
