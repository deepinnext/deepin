using Deepin.Application.Models;

namespace Deepin.Application.Queries;

public interface INoteQueries
{
    Task<NoteDto?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<PagedResult<NoteDto>?> GetListAsync(string userId, NoteQuery noteQuery, CancellationToken cancellationToken = default);
}
