using Deepin.Domain.PostAggregates;
using MediatR;

namespace Deepin.Application.Commands.Posts;

public class DeletePostCommandHandler(IPostRepository postRepository) : IRequestHandler<DeletePostCommand, bool>
{
    private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    public async Task<bool> Handle(DeletePostCommand request, CancellationToken cancellationToken)
    {
        var post = await _postRepository.GetByIdAsync(request.Id, cancellationToken);
        if (post is null)
        {
            return false;
        }
        _postRepository.Delete(post);
        await _postRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
        return true;
    }
}
