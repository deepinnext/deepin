using MediatR;

namespace Deepin.Application.Commands.Categories;

public record CreateCategoryCommand(string Name, string Description) : IRequest<int>;