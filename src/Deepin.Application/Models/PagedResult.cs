using System;

namespace Deepin.Application.Models;

public class PagedResult<T>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }
    public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);
    public bool HasPreviousPage => PageIndex > 1;
    public bool HasNextPage => PageIndex < TotalPages;
    public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
    public PagedResult(int pageIndex, int pageSize, IQueryable<T> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = items.Count();
        Items = items.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
    }
    public PagedResult(int pageIndex, int pageSize, int totalCount, IEnumerable<T> items)
    {
        PageIndex = pageIndex;
        PageSize = pageSize;
        TotalCount = totalCount;
        Items = items;
    }
}
