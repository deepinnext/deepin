using MediatR;

namespace Deepin.Application.Commands.Files;

public record DownloadFileCommand(Guid Id) : IRequest<Tuple<FileDTO, Stream>?>;
