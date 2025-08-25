namespace CorpResource.Components.Shared;

public static class TableFilter
{
    public static IEnumerable<T> ApplySearch<T>(IEnumerable<T> source, string search, params Func<T, string>[] selectors)
    {
        if (string.IsNullOrWhiteSpace(search))
            return source;

        return source.Where(item =>
            selectors.Any(sel =>
            {
                var value = sel(item);
                return !string.IsNullOrEmpty(value) &&
                       value.Contains(search, StringComparison.OrdinalIgnoreCase);
            })
        );
    }
}
