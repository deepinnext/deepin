using Deepin.Application.Commands.Tags;
using Deepin.Domain.PostAggregates;
using MediatR;

namespace Deepin.Application.Commands.Posts;

public class UpdatePostCommandHandler(IMediator mediator, IPostRepository postRepository) : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post is null)
        {
            return false;
        }
        post.ClearTags();
        if (request.Tags is not null)
        {
            foreach (var tagName in request.Tags)
            {
                var tag = await _mediator.Send(new GetOrCreateTagCommand(tagName!), cancellationToken);
                post.AddTag(tag.Id);
            }
        }
        post.Update(request.Content);
        _postRepository.Update(post);
        await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return true;
    }
}