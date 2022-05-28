namespace Utilities_aspnet.Utilities;

public static class EnumExtension {
    public static List<IdTitleReadDto> GetValues<T>() {
        return (from int itemType in Enum.GetValues(typeof(T))
            select new IdTitleReadDto {Title = Enum.GetName(typeof(T), itemType), SecondaryId = itemType}).ToList();
    }

    public static Guid ToGuid(int value) {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(value).CopyTo(bytes, 0);
        return new Guid(bytes);
    }
}