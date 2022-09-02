﻿using Microsoft.EntityFrameworkCore;

namespace Core.Persistence.Paging;

public static class IQueryablePaginateExtensions
{
    public static IPaginate<T> ToPaginate<T>(this IQueryable<T> source, int index, int size, int from = 0)
    {
        if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

        var count = source.Count();
        var items = source.Skip((index - from) * size).Take(size).ToList();

        return new Paginate<T>()
        {
            Index = index,
            Size = size,
            From = from,
            Count = count,
            Items = items,
            Pages = (int)Math.Ceiling(count / (double)size)
        };
    }

    public static async Task<IPaginate<T>> ToPaginateAsync<T>(this IQueryable<T> source, int index, int size, int from = 0, CancellationToken cancellationToken = default)
    {
        if (from > index) throw new ArgumentException($"From: {from} > Index: {index}, must From <= Index");

        var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
        var items = await source.Skip((index - from) * size).Take(size).ToListAsync(cancellationToken).ConfigureAwait(false);

        return new Paginate<T>()
        {
            Index = index,
            Size = size,
            From = from,
            Items = items,
            Pages = (int)Math.Ceiling(count / (double)size)
        };
    }
}