namespace Core.Persistence.Paging;

public class Paginate<T> : IPaginate<T>
{
    public int From { get; set; }
    public int Index { get; set; }
    public int Size { get; set; }
    public int Count { get; set; }
    public int Pages { get; set; }
    public IList<T> Items { get; set; }
    public bool HasPrevious => Index - From > 0;
    public bool HasNext => Index - From + 1 < Pages;

    public Paginate() { Items = new T[0]; }

    public Paginate(IEnumerable<T> source, int index, int size, int from)
    {
        if (from > index) throw new ArgumentException($"Index From: {from} > Page Index: {index}, must Index From <= Page Index");

        Index = index;
        Size = size;
        From = from;

        if (source is IQueryable<T> queryable)
        {
            Count = queryable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = queryable.Skip((Index - From) * Size).Take(Size).ToList();
        }
        else
        {
            var enumerable = source as T[] ?? source.ToArray();

            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);
            Items = enumerable.Skip((Index - From) * Size).Take(Size).ToList();
        }
    }
}

public class Paginate<TSource, TResult> : IPaginate<TResult>
{
    public int From { get; }
    public int Index { get; }
    public int Size { get; }
    public int Count { get; }
    public int Pages { get; }
    public IList<TResult> Items { get; }
    public bool HasPrevious => Index - From > 0;
    public bool HasNext => Index - From + 1 < Pages;

    public Paginate(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter)
    {
        From = source.From;
        Index = source.Index;
        Size = source.Size;
        Count = source.Count;
        Pages = source.Pages;
        Items = converter(source.Items).ToList();
    }

    public Paginate(IEnumerable<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from)
    {
        if (from > index) throw new ArgumentException($"Index From: {from} > Page Index: {index}, must Index From <= Page Index");

        Index = index;
        Size = size;
        From = from;

        if (source is IQueryable<TSource> queryable)
        {
            Count = queryable.Count();
            Pages = (int)Math.Ceiling(Count / (double)Size);

            var items = queryable.Skip((Index - From) * Size).Take(Size).ToArray();

            Items = converter(items).ToList();
        }
        else
        {
            var enumerable = source as TSource[] ?? source.ToArray();

            Count = enumerable.Length;
            Pages = (int)Math.Ceiling(Count / (double)Size);

            var items = enumerable.Skip((Index-From)* Size).Take(Size).ToArray();

            Items = converter(items).ToList();
        }
    }
}

public static class Paginate
{
    public static IPaginate<T> Empty<T>() => new Paginate<T>();

    public static IPaginate<TResult> From<TResult, TSource>(IPaginate<TSource> source, Func<IEnumerable<TSource>, IEnumerable<TResult>> converter) => new Paginate<TSource, TResult>(source, converter);
}