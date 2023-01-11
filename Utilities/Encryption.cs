using System.Security.Cryptography;

namespace Utilities_aspnet.Utilities;

public class Encryption {

	public static string GetMd5HashData(string data) {
		MD5 md5 = MD5.Create();
		byte[] inputBytes = Encoding.ASCII.GetBytes(data);
		byte[] hashBytes = md5.ComputeHash(inputBytes);

		StringBuilder sb = new();
		foreach (byte t in hashBytes) {
			sb.Append(t.ToString("X2"));
		}
		return sb.ToString();
	}

	public static bool ValidateMd5HashData(string inputData, string storedHashData) {
		byte[] tmpSource = ASCIIEncoding.ASCII.GetBytes(inputData);
		byte[] tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);

		bool bEqual = false;
		if (tmpNewHash.Length == storedHashData.Length) {
			int i = 0;
			while (i < tmpNewHash.Length && tmpNewHash[i] == storedHashData[i]) i += 1;
			if (i == tmpNewHash.Length) bEqual = true;
		}

		return bEqual;
	}
}