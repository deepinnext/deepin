using AutoMapper;
using Deepin.Application.Commands.Tags;
using Deepin.Application.Queries;
using Deepin.Application.Services;
using Deepin.Domain.NoteAggregates;
using Deepin.Domain.PageAggregates;
using MediatR;

namespace Deepin.Application.Commands.Notes;

public class CreateNoteCommandHandler(IMediator mediator,IMapper mapper, INoteRepository noteRepository, IUserContext userContext) : IRequestHandler<CreateNoteCommand, NoteDto>
{
    private readonly IMediator _mediator = mediator;
    private readonly IMapper _mapper = mapper;
    private readonly INoteRepository _noteRepository = noteRepository;
    private readonly IUserContext _userContext = userContext;
    public async Task<NoteDto> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
    {
        var note = new Note(
            userId: _userContext.UserId,
            categoryId: request.CategoryId,
            title: request.Title,
            content: request.Content,
            isPublic: request.IsPublic,
            description: request.Description,
            coverImageId: request.CoverImageId);
        if (request.Tags is not null)
        {
            foreach (var tagName in request.Tags)
            {
                var tag = await _mediator.Send(new GetOrCreateTagCommand(tagName!), cancellationToken);
                note.AddTag(tag.Id);
            }
        }

        // Publish post
        if (request.IsPublished)
        {
            note.Publish();
        }
        await _noteRepository.AddAsync(note, cancellationToken);
        await _noteRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return _mapper.Map<NoteDto>(note);
    }
}
