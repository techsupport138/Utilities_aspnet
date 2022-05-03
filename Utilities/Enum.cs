using Utilities_aspnet.Utilities.Dtos;

namespace Utilities_aspnet.Utilities {
    public static class EnumExtension {
        public static List<IdTitleDto> GetValues<T>() {
            return (from object itemType in Enum.GetValues(typeof(T))
                select new IdTitleDto() {Title = Enum.GetName(typeof(T), itemType), Id = itemType.ToString()}).ToList();
        }
    }
}