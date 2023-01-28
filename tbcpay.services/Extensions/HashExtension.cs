using System.Security.Cryptography;
using System.Text;

namespace tbcpay.services.Extensions
{
    public static class HashExtension
    {
        public static string ComputeSha256Hash(this string jsonBody, string secret)
            {
                {
                    {
                        using var sha256Hash = SHA256.Create();
                        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(jsonBody + secret));
                        var stringBuilder = new StringBuilder();
                        foreach (var t in bytes)
                            stringBuilder.Append(t.ToString("X2"));
                        return stringBuilder.ToString();
                    }
                }
            }
        
    }
}