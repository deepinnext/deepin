using MediatR;

namespace Deepin.Application.Commands.Files;

public record UploadFileCommand(string ContentType, string FileName, long Length, Stream FileStream) : IRequest<FileDTO>;
