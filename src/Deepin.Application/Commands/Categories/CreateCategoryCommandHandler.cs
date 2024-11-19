using Deepin.Application.Services;
using Deepin.Domain.Entities;
using Deepin.Infrastructure;
using MediatR;

namespace Deepin.Application.Commands.Categories;

public class CreateCategoryCommandHandler(IUserContext userContext, DeepinDbContext dbContext) : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly IUserContext _userContext = userContext;
    private readonly DeepinDbContext _dbContext = dbContext;
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = new Category(_userContext.UserId, request.Name, request.Description);
        await _dbContext.Categories.AddAsync(category, cancellationToken);
        await _dbContext.SaveEntitiesAsync(cancellationToken);
        return category.Id;
    }
}
