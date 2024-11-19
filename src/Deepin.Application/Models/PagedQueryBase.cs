namespace Deepin.Application.Models;

public abstract class PagedQueryBase
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; } = 20;
    public string? Search { get; set; }
}
