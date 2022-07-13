namespace Utilities_aspnet.Utilities;

public static class NumberExtension {
	public static bool IsNumerical(this string value) => Regex.IsMatch(value, @"^\d+$");

	public static bool IsMobileNumber(this string value) => value.IsNumerical() && value.Length >= 9 && value.Contains('0');

	public static int ToInt(this double value) => (int) value;
	public static int ToInt(this double? value) => (int) (value ?? 0.0);
}