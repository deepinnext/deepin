using Deepin.Application.Commands.Tags;
using Deepin.Domain.NoteAggregates;
using MediatR;

namespace Deepin.Application.Commands.Notes;

public class UpdateNoteCommandHandler(IMediator mediator,INoteRepository noteRepository) : IRequestHandler<UpdateNoteCommand, bool>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly INoteRepository _noteRepository = noteRepository ?? throw new ArgumentNullException(nameof(noteRepository));
    public async Task<bool> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = await _noteRepository.GetByIdAsync(request.Id, cancellationToken);
        if (note is null)
        {
            return false;
        }
        note.ClearTags();
        if (request.Tags is not null)
        {
            foreach (var tagName in request.Tags)
            {
                var tag = await _mediator.Send(new GetOrCreateTagCommand(tagName!), cancellationToken);
                note.AddTag(tag.Id);
            }
        }
        note.Update(request.Title, request.Content, request.IsPublic, request.Description, request.CoverImageId, request.IsPublished);
        _noteRepository.Update(note);
        await _noteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return true;
    }
}
