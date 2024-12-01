using MediatR;

namespace Deepin.Application.Commands.Posts;

public record CreatePostCommand(string Content, string[]? Tags = null) : IRequest<int>;