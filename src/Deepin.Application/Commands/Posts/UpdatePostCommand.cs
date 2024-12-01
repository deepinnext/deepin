using MediatR;

namespace Deepin.Application.Commands.Posts;

public class UpdatePostCommand : IRequest<bool>
{
    public int Id { get; set; }
    public string Content { get; set; } = string.Empty;
    public string[]? Tags { get; set; }
}