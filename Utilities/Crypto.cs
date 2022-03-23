using System.Security.Cryptography;
using System.Text;

namespace Utilities_aspnet.Utilities;

public class Crypto {
    public static string ToSha256(string s) {
        using SHA256 sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
        StringBuilder sb = new();

        foreach (byte t in bytes) {
            sb.Append(t.ToString("x2"));
        }
        return sb.ToString();
    }
}