using System;

namespace Deepin.Application.Queries;

public interface ITagQueries
{
    Task<TagDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<IEnumerable<TagDto>?> GetListAsync(CancellationToken cancellationToken);
}
