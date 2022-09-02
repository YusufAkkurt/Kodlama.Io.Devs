using System.Linq.Dynamic.Core;
using System.Text;

namespace Core.Persistence.Dynamics;

public static class IQueryableDynamicFilterExtensions
{
    private static readonly IDictionary<string, string> Operators = new Dictionary<string, string>
    {
        { "eq", "=" },
        { "neq", "!=" },
        { "lt", "<" },
        { "lte", "<=" },
        { "gt", ">" },
        { "gte", ">=" },
        { "isnull", "== null" },
        { "isnotnull", "!= null" },
        { "startswith", "StartsWith" },
        { "endswith", "EndsWith" },
        { "contains", "Contains" },
        { "doesnotcontains", "Contains" }
    };

    private static IList<Filter> GetAllFilters(Filter filter)
    {
        var filters = new List<Filter>();
        GetFilters(filter, filters);

        return filters;
    }

    private static void GetFilters(Filter filter, List<Filter> filters)
    {
        filters.Add(filter);

        if (filter.Filters is not null && filter.Filters.Any())
            foreach (var item in filter.Filters)
                GetFilters(item, filters);
    }

    private static string Transform(Filter filter, IList<Filter> filters)
    {
        var index = filters.IndexOf(filter);
        string comprasion = Operators[filter.Operator];
        var where = new StringBuilder();

        if (!string.IsNullOrEmpty(filter.Value))
        {
            if (filter.Operator == "doesnotcontain") where.Append($"(!np({filter.Field}).{comprasion}(@{index}))");
            else if (comprasion == "StartsWith" || comprasion == "EndsWith" || comprasion == "Contains") where.Append($"(np({filter.Field}).{comprasion}(@{index}))");
            else where.Append($"np({filter.Field}) {comprasion} @{index}");
        }
        else if (filter.Operator == "isnull" || filter.Operator == "isnotnull") where.Append($"np({filter.Field}) {comprasion}");

        return (filter.Logic is not null && filter.Filters is not null && filter.Filters.Any())
            ? $"{where} {filter.Logic} ({string.Join($" {filter.Logic} ", filter.Filters.Select(innerFilter => Transform(innerFilter, filters)).ToArray())})"
            : where.ToString();
    }

    private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
    {
        var filters = GetAllFilters(filter);
        string?[] values = filters.Select(innerFilter => innerFilter.Value).ToArray();
        string where = Transform(filter, filters);

        return queryable.Where(where, values);
    }

    private static IQueryable<T> Sort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
    {
        if (!sort.Any()) return queryable;

        var oredering = string.Join(',', sort.Select(innerSort => $"{innerSort.Field} {innerSort.Dir}"));

        return queryable.OrderBy(oredering);
    }

    public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, Dynamic dynamic)
    {
        if (dynamic.Filter is not null) query = Filter(query, dynamic.Filter);
        if (dynamic.Sorts is not null && dynamic.Sorts.Any()) query = Sort(query, dynamic.Sorts);

        return query;
    }
}