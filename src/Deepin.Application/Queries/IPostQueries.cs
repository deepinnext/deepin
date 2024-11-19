using Deepin.Application.Models;
using Deepin.Domain.PostAggregates;

namespace Deepin.Application.Queries;

public interface IPostQueries
{
    Task<PostDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PagedResult<PostDto>?> GetListAsync(PostPagedQuery pagedQuery, CancellationToken cancellationToken = default);
}
