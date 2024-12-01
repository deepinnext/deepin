using MediatR;

namespace Deepin.Application.Commands.Notes;

public record DeleteNoteCommand(int Id) : IRequest<bool>;