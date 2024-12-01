using Deepin.Application.Commands.Tags;
using Deepin.Application.Services;
using Deepin.Domain.PostAggregates;
using MediatR;

namespace Deepin.Application.Commands.Posts;

public class CreatePostCommandHandler(
    IMediator mediator,
    IPostRepository postRepository,
    IUserContext userContext) : IRequestHandler<CreatePostCommand, int>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    private readonly IUserContext _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));

    public async Task<int> Handle(CreatePostCommand request, CancellationToken cancellationToken)
    {
        var post = new Post(content: request.Content, createdBy: _userContext.UserId);

        // Add tags
        if (request.Tags is not null)
        {
            foreach (var tagName in request.Tags)
            {
                var tag = await _mediator.Send(new GetOrCreateTagCommand(tagName!), cancellationToken);
                post.AddTag(tag.Id);
            }
        }

        await _postRepository.AddAsync(post, cancellationToken);
        await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return post.Id;
    }
}
