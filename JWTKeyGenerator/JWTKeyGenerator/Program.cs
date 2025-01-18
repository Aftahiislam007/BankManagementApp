// See https://aka.ms/new-console-template for more information
/*Console.WriteLine("Hello, World!");*/

using System.Security.Cryptography;

var key = new byte[32];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(key);
}
string base64Key = Convert.ToBase64String(key);
Console.WriteLine($"My JWT Key: {base64Key}");
