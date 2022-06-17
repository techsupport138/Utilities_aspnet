namespace Utilities_aspnet.Utilities;

public static class EnumerableExtension {
    public static bool IsNotNullOrEmpty<T>(this IEnumerable<T>? list) {
        return list != null && list.Any();
    }

    public static bool IsNullOrEmpty<T>(this IEnumerable<T>? list) {
        return list == null || !list.Any();
    }
}