using MediatR;

namespace Deepin.Application.Commands.Posts;

public record CreatePostCommand(string Title, string Content, int[] TagIds, int[] CategoryIds, string? Summary = null, bool IsPublished = false) : IRequest<int>;