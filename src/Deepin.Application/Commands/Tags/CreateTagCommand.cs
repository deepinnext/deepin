using MediatR;

namespace Deepin.Application.Commands.Tags;

public record CreateTagCommand(string Name, string Description) : IRequest<int>;
