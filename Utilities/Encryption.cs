namespace Utilities_aspnet.Utilities;

public class Encryption {
	public static string GetMd5HashData(string data) {
		StringBuilder sb = new();
		foreach (byte t in MD5.Create().ComputeHash(Encoding.ASCII.GetBytes(data))) sb.Append(t.ToString("X2"));
		return sb.ToString();
	}

	public static bool ValidateMd5HashData(string inputData, string storedHashData) {
		byte[] tmpNewHash = new MD5CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(inputData));

		bool bEqual = false;
		if (tmpNewHash.Length == storedHashData.Length) {
			int i = 0;
			while (i < tmpNewHash.Length && tmpNewHash[i] == storedHashData[i]) i += 1;
			if (i == tmpNewHash.Length) bEqual = true;
		}

		return bEqual;
	}
}