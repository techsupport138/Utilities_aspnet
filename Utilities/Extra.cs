namespace Utilities_aspnet.Utilities;

public static class BoolExtension {
	public static bool IsNullOrFalse(this bool? value) {
		switch (value) {
			case null:
			case false: return true;
			default: return false;
		}
	}
}