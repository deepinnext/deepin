using MediatR;

namespace Deepin.Application.Commands.Notes;

public record ArchiveNoteCommand(int Id) : IRequest<bool>;
