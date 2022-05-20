namespace Utilities_aspnet.Utilities;

public static class NumberExtension {
    public static bool isNumerical(this string value) {
        return Regex.IsMatch(value, @"^\d+$");
    }

    public static bool isMobileNumber(this string value) {
        return value.isNumerical() && value.Length >= 9 && value.Contains('0');
    }
}