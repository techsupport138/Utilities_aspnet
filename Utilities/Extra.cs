namespace Utilities_aspnet.Utilities;

public static class BoolExtension {
	public static bool IsNullOrFalse(this bool? value) => value == null && value == false;

	public static bool IsTrue(this bool? value) => value != null && value != false;
}

public class Utils {
	public static int Random(int codeLength) {
		Random rnd = new();
		int otp = codeLength switch {
			4 => rnd.Next(1001, 9999),
			5 => rnd.Next(1001, 99999),
			_ => rnd.Next(1001, 9999)
		};

		return otp;
	}
}