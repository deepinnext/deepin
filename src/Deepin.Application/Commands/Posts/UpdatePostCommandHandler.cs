using Deepin.Domain.PostAggregates;
using MediatR;

namespace Deepin.Application.Commands.Posts;

public class UpdatePostCommandHandler(IPostRepository postRepository) : IRequestHandler<UpdatePostCommand, bool>
{
    private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    public async Task<bool> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post is null)
        {
            return false;
        }
        post.Update(request.Title, request.Content, request.Summary, request.IsPublic);
        post.UpdateTags(request.TagIds);
        post.UpdateCategories(request.CategoryIds);
        _postRepository.Update(post);
        await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return true;
    }
}