namespace Utilities_aspnet.Utilities;

public static class NumberExtension {
	public static bool IsNumerical(this string value) => Regex.IsMatch(value, @"^\d+$");
	
	public static int ToInt(this double value) => (int) value;
	public static int ToInt(this double? value) => (int) (value ?? 0.0);
}