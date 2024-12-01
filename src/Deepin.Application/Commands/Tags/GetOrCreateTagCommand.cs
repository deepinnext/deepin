using Deepin.Application.Queries;
using MediatR;

namespace Deepin.Application.Commands.Tags;

public record GetOrCreateTagCommand(string Name) : IRequest<TagDto>;
