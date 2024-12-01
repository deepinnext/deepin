using Deepin.Domain.PageAggregates;

namespace Deepin.Domain.NoteAggregates;

public interface INoteRepository : IRepository<Note>
{
    Task AddAsync(Note note, CancellationToken cancellationToken = default);
    Task<Note?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    void Update(Note post);
    void Delete(Note post);
}