using Deepin.Domain.NoteAggregates;
using MediatR;

namespace Deepin.Application.Commands.Notes;

public class DeleteNoteCommandHandler(INoteRepository noteRepository) : IRequestHandler<DeleteNoteCommand, bool>
{
    private readonly INoteRepository _noteRepository= noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    public async Task<bool> Handle(DeleteNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id, cancellationToken);
        if (note is null)
        {
            return false;
        }
        note.Delete();
        _noteRepository.Update(note);
        await _noteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return true;
    }
}
