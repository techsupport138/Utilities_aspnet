namespace Utilities_aspnet.Utilities;

public static class BoolExtension {
	public static bool IsNullOrFalse(this bool? value) => value == null && value == false;

	public static bool IsTrue(this bool? value) => value != null && value != false;
}