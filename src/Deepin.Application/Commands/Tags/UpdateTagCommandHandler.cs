using Deepin.Domain;
using Deepin.Infrastructure;
using MediatR;

namespace Deepin.Application.Commands.Tags;

public class UpdateTagCommandHandler(DeepinDbContext dbContext) : IRequestHandler<UpdateTagCommand, Unit>
{
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _dbContext.Tags.FindAsync(request.Id);
        if (tag is null)
        {
            throw new DomainException($"Tag with id {request.Id} not found.");
        }
        tag.Update(request.Name, request.Description);
        await _dbContext.SaveEntitiesAsync(cancellationToken);
        return Unit.Value;
    }
}
