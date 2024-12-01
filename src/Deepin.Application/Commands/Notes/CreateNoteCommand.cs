using Deepin.Application.Queries;
using MediatR;

namespace Deepin.Application.Commands.Notes;

public record CreateNoteCommand(
    int CategoryId,
    string Title,
    string Content,
    bool IsPublic = false,
    string? Description = null,
    Guid? CoverImageId = null,
    bool IsPublished = false,
    string[]? Tags = null) : IRequest<NoteDto>;
