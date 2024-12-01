using MediatR;

namespace Deepin.Application.Commands.Tags;

public class UpdateTagCommand : IRequest<Unit>
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}
