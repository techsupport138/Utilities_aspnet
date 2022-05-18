namespace Utilities_aspnet.Utilities {
    public static class EnumExtension {
        public static List<IdTitleReadDto> GetValues<T>() {
            return (from Guid itemType in Enum.GetValues(typeof(T))
                select new IdTitleReadDto {Title = Enum.GetName(typeof(T), itemType), Id = itemType}).ToList();
        }
    }
}