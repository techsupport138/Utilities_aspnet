namespace Utilities_aspnet.Utilities; 

public static class EnumExtension {
    public static List<KVIdTitle> GetValues<T>() {
        return (from int itemType in Enum.GetValues(typeof(T))
            select new KVIdTitle { Title = Enum.GetName(typeof(T), itemType), Id = itemType}).ToList();
    }
}