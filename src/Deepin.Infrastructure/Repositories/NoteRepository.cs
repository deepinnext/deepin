using Deepin.Domain;
using Deepin.Domain.NoteAggregates;
using Deepin.Domain.PageAggregates;

namespace Deepin.Infrastructure.Repositories;

public class NoteRepository(DeepinDbContext deepinDbContext) : INoteRepository
{
    private readonly DeepinDbContext _dbContext = deepinDbContext;
    public IUnitOfWork UnitOfWork => _dbContext;

    public async Task AddAsync(Note note, CancellationToken cancellationToken = default)
    {
        await _dbContext.Notes.AddAsync(note, cancellationToken);
    }

    public void Delete(Note post)
    {
        _dbContext.Notes.Remove(post);
    }

    public async Task<Note?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Notes.FindAsync([id], cancellationToken);
    }

    public void Update(Note post)
    {
        _dbContext.Notes.Update(post);
    }
}
