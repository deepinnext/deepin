using Deepin.Application.Models;

namespace Deepin.Application.Queries;

public interface IPostQueries
{
    Task<PostDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PagedResult<PostDto>?> GetListAsync(string userId, PostQuery postQuery, CancellationToken cancellationToken = default);
}
