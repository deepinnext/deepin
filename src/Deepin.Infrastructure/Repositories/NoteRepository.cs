using Deepin.Domain;
using Deepin.Domain.NoteAggregates;
using Deepin.Domain.PageAggregates;

namespace Deepin.Infrastructure.Repositories;

public class NoteRepository : INoteRepository
{
    public IUnitOfWork UnitOfWork => throw new NotImplementedException();

    public Task AddAsync(Note note, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Delete(Note post)
    {
        throw new NotImplementedException();
    }

    public Task<Note?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public void Update(Note post)
    {
        throw new NotImplementedException();
    }
}
