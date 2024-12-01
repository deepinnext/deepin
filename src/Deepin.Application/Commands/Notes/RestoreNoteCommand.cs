using MediatR;

namespace Deepin.Application.Commands.Notes;

public record RestoreNoteCommand(int Id) : IRequest<bool>;