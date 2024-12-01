using Deepin.Domain.NoteAggregates;
using MediatR;

namespace Deepin.Application.Commands.Notes;

public class RestoreNoteCommandHandler(INoteRepository noteRepository) : IRequestHandler<RestoreNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    public async Task<bool> Handle(RestoreNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id, cancellationToken);
        if (note is null)
        {
            return false;
        }
        note.Restore();
        _noteRepository.Update(note);
        await _noteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return true;
    }
}
