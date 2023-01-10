using System.Security.Cryptography;

namespace Utilities_aspnet.Utilities;

public class Encryption {
	public static string GetMd5HashData(string data) {
		MD5 md5 = MD5.Create();
		byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
		StringBuilder returnValue = new();
		foreach (byte t in hashData) {
			returnValue.Append(t.ToString());
		}
		return returnValue.ToString();
	}

	public static bool ValidateMd5HashData(string inputData, string storedHashData) {
		string getHashInputData = GetMd5HashData(inputData);
		return string.CompareOrdinal(getHashInputData, storedHashData) == 0;
	}
}