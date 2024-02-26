namespace TaskManager.Domain.Helpers;

public static class Helper
{
    public static IReadOnlyList<T>? ToNullableReadOnlyList<T>(this IEnumerable<T>? collection)
        => collection as IReadOnlyList<T> ?? collection?.ToArray();

    public static IReadOnlyList<T> ToReadOnlyList<T>(this IEnumerable<T>? collection)
        => collection.ToNullableReadOnlyList() ?? Array.Empty<T>();
}