using System;
using Deepin.Application.Queries;
using Deepin.Infrastructure;
using MediatR;

namespace Deepin.Application.Commands.Files;

public class GetFileQueryHandler(DeepinDbContext dbContext) : IRequestHandler<GetFileQuery, FileDTO?>
{
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<FileDTO?> Handle(GetFileQuery request, CancellationToken cancellationToken)
    {
        var file = await _dbContext.FileObjects.FindAsync(request.Id);
        return file is null ? null : FileDTO.FromEntity(file);
    }
}
