using Deepin.Application.Services;
using Deepin.Domain;
using Deepin.Domain.Entities;
using Deepin.Infrastructure;
using Deepin.Infrastructure.FileStorage;
using HeyRed.Mime;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Deepin.Application.Commands.Files;

public class UploadFileCommandHandler(
    ILogger<UploadFileCommandHandler> logger,
    IFileStorage fileStorage,
    IUserContext userContext,
    DeepinDbContext dbContext) : IRequestHandler<UploadFileCommand, FileDTO>
{
    private readonly ILogger<UploadFileCommandHandler> _logger = logger;
    private readonly IFileStorage _fileStorage = fileStorage;
    private readonly IUserContext _userContext = userContext;
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<FileDTO> Handle(UploadFileCommand request, CancellationToken cancellationToken)
    {
        var transaction = await _dbContext.BeginTransactionAsync();
        try
        {
            var entity = new FileObject(request.FileName, MimeTypesMap.GetExtension(request.ContentType), request.Length, _userContext.UserId);
            await _dbContext.FileObjects.AddAsync(entity);
            await _dbContext.SaveChangesAsync(cancellationToken);
            await _fileStorage.CreateAsync(entity, request.FileStream);
            await transaction.CommitAsync();
            return FileDTO.FromEntity(entity);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error on upload file");
            await transaction.RollbackAsync();
            throw new DomainException("Error on upload file", ex);
        }
    }
}
