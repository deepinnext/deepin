using Deepin.Application.Queries;
using MediatR;

namespace Deepin.Application.Commands.Tags;

public class GetOrCreateTagCommandHandler(IMediator mediator, ITagQueries tagQueries) : IRequestHandler<GetOrCreateTagCommand, TagDto>
{
    private readonly IMediator _mediator = mediator;
    private readonly ITagQueries _tagQueries = tagQueries;
    public async Task<TagDto> Handle(GetOrCreateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _tagQueries.GetByNameAsync(request.Name, cancellationToken);
        if (tag is not null)
        {
            return tag;
        }
        var tagId = await _mediator.Send(new CreateTagCommand(request.Name, string.Empty), cancellationToken);
        return await _tagQueries.GetByIdAsync(tagId, cancellationToken) ?? throw new InvalidOperationException();
    }
}
