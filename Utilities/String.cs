namespace Utilities_aspnet.Utilities;

public static class String {
	public static bool IsEmail(this string email) {
		const string regex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
		return Regex.IsMatch(email, regex, RegexOptions.IgnoreCase);
	}
}