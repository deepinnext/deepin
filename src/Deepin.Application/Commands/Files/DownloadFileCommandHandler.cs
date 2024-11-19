using Deepin.Infrastructure;
using Deepin.Infrastructure.FileStorage;
using MediatR;

namespace Deepin.Application.Commands.Files;

public class DownloadFileCommandHandler(IFileStorage fileStorage, DeepinDbContext dbContext) : IRequestHandler<DownloadFileCommand, Tuple<FileDTO, Stream>?>
{
    private readonly IFileStorage _fileStorage = fileStorage;
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<Tuple<FileDTO, Stream>?> Handle(DownloadFileCommand request, CancellationToken cancellationToken)
    {
        var file = await _dbContext.FileObjects.FindAsync(request.Id);
        if (file is null) return null;
        var stream = await _fileStorage.GetStreamAsync(file);
        return Tuple.Create(FileDTO.FromEntity(file), stream);
    }
}
