using MediatR;

namespace Deepin.Application.Commands.Posts;

public record DeletePostCommand(int Id) : IRequest<bool>;
