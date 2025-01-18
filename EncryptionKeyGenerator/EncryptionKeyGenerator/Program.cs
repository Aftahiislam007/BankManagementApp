// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/


using System.Security.Cryptography;

var key = GenerateEncryptionKey();
Console.WriteLine($"My AES Encryption Key: {key}");

static string GenerateEncryptionKey()
{
    using var rng = RandomNumberGenerator.Create();
    var keyBytes = new byte[32];
    rng.GetBytes(keyBytes);
    return Convert.ToBase64String(keyBytes);
}