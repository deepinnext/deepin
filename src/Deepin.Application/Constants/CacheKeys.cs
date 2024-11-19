namespace Deepin.Application.Constants;
public static class CacheKeys
{
    public static string GetFileById(Guid id) => $"file_{id}";
    public static string GetAllCategories() => "categories";
    public static string GetAllTags() => "tags";
}