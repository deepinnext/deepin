using MediatR;

namespace Deepin.Application.Commands.Notes;

public record CreateNoteCommand(
    string UserId,
    int CategoryId,
    string Title,
    string Content,
    bool IsPublic = false,
    string? Description = null,
    Guid? CoverImageId = null,
    bool IsPublished = false,
    string[]? Tags = null) : IRequest<int>;
