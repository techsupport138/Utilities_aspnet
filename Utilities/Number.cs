namespace Utilities_aspnet.Utilities;

public static class NumberExtension {
	public static bool IsNumerical(this string value) => Regex.IsMatch(value, @"^\d+$");

	public static bool IsMobileNumber(this string value) => value.IsNumerical() && value.Length >= 9 && value.Contains('0');

	public static bool IsEmail(this string email) {
		string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

		return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
	}
}