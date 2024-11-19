using Deepin.Domain;
using Deepin.Infrastructure;
using MediatR;

namespace Deepin.Application.Commands.Categories;

public class UpdateCategoryCommandHandler(DeepinDbContext dbContext) : IRequestHandler<UpdateCategoryCommand, Unit>
{
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<Unit> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _dbContext.Categories.FindAsync(request.Id);
        if (category is null)
        {
            throw new DomainException($"Category with id {request.Id} not found.");
        }
        category.Update(request.Name, request.Description);
        await _dbContext.SaveEntitiesAsync(cancellationToken);
        return Unit.Value;
    }
}
