using System.Text;
using System.Security.Cryptography;

namespace BankManagement.Utils
{
    public static class EncryptionHelper
    {
        private static readonly string Key = Environment.GetEnvironmentVariable("EncryptionKey")
            ?? throw new InvalidOperationException("Encryption key is not configured.");

        public static string Encrypt(string plainText)
        {
            /*var aesKey = GetValidKey(Key);*/
            using var aes = Aes.Create();

            aes.Key = Encoding.UTF8.GetBytes(Key);
            /*aes.Key = aesKey;*/

            aes.IV = new byte[16];

            using var encryptor = aes.CreateEncryptor();
            var plainBytes = Encoding.UTF8.GetBytes(plainText);
            var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string cipherText)
        {
            /*var aesKey = GetValidKey(Key);*/
            using var aes = Aes.Create();

            aes.Key = Encoding.UTF8.GetBytes(Key);
            /*aes.Key = aesKey;*/

            aes.IV = new byte[16];

            using var decryptor = aes.CreateDecryptor();
            var cipherBytes = Convert.FromBase64String(cipherText);
            var decryptedBytes = decryptor.TransformFinalBlock(cipherBytes, 0, cipherBytes.Length);

            return Encoding.UTF8.GetString(decryptedBytes);
        }

        private static byte[] GetValidKey(string key)
        {
            var keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32);
            }
            else if (keyBytes.Length > 32)
            {
                keyBytes = keyBytes.Take(32).ToArray();
            }
            return keyBytes;
        }
    }
}
