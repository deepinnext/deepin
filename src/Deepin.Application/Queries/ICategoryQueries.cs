namespace Deepin.Application.Queries;

public interface ICategoryQueries
{
    Task<CategoryDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<CategoryDto>?> GetListAsync(CancellationToken cancellationToken);
}
