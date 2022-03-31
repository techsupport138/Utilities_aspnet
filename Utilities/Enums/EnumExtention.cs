using Utilities_aspnet.Models.Dto;

namespace Utilities_aspnet.Extensions
{
    public static class EnumExtension
    {
        public static List<IdTitleDto> GetValues<T>()
        {
            List<IdTitleDto> values = new List<IdTitleDto>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                values.Add(new IdTitleDto()
                {
                    Title = Enum.GetName(typeof(T), itemType),
                    Id = (int)itemType
                });
            }
            return values;
        }
    }
}