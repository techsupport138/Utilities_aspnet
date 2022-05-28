namespace Utilities_aspnet.Utilities;

public static class EnumExtension {
    public static List<IdTitleReadDto> GetValues<T>() {
        return (from int itemType in Enum.GetValues(typeof(T))
            select new IdTitleReadDto {Title = Enum.GetName(typeof(T), itemType), SecondaryId = itemType}).ToList();
    }
}