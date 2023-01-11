using System.Security.Cryptography;

namespace Utilities_aspnet.Utilities;

public class Encryption {
	// public static string GetMd5HashData(string data) {
	// 	MD5 md5 = MD5.Create();
	// 	byte[] hashData = md5.ComputeHash(Encoding.Default.GetBytes(data));
	// 	StringBuilder returnValue = new();
	// 	foreach (byte t in hashData) {
	// 		returnValue.Append(t.ToString());
	// 	}
	// 	return returnValue.ToString();
	// }
	//
	// public static bool ValidateMd5HashData(string inputData, string storedHashData) {
	// 	string getHashInputData = GetMd5HashData(inputData);
	// 	return string.CompareOrdinal(getHashInputData, storedHashData) == 0;
	// }
	
	public static string GetMd5HashData(string data) {
		MD5 md5 = System.Security.Cryptography.MD5.Create();
		byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(data);
		byte[] hashBytes = md5.ComputeHash(inputBytes);
            
		// Step 2, convert byte array to hex string
		StringBuilder sb = new StringBuilder();
		for (int i = 0; i < hashBytes.Length; i++)
		{
			sb.Append(hashBytes[i].ToString("X2"));
		}
		return sb.ToString();
	}

	public static bool ValidateMd5HashData(string inputData, string storedHashData) {
		string getHashInputData = GetMd5HashData(inputData);
		return string.CompareOrdinal(getHashInputData, storedHashData) == 0;
	}
}