using Deepin.Application.Services;
using Deepin.Domain.Entities;
using Deepin.Infrastructure;
using MediatR;

namespace Deepin.Application.Commands.Tags;

public class CreateTagCommandHandler(IUserContext userContext, DeepinDbContext dbContext) : IRequestHandler<CreateTagCommand, int>
{
    private readonly IUserContext _userContext = userContext;
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<int> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = new Tag(_userContext.UserId, request.Name, request.Description);
        await _dbContext.Tags.AddAsync(tag, cancellationToken);
        await _dbContext.SaveEntitiesAsync(cancellationToken);
        return tag.Id;
    }
}