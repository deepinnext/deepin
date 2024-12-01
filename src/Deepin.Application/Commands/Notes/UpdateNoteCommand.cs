using MediatR;

namespace Deepin.Application.Commands.Notes;

public class UpdateNoteCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public string? Description { get; set; }
    public bool? IsPublished { get; set; }
    public Guid? CoverImageId { get; set; }
    public string[]? Tags { get; set; }
}
